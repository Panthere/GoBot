
using AllEnum;
using GoBot.Utils;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.GeneratedCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBot.Logic
{
    public class BotInstance
    {
        public readonly Client _client;
        private readonly ISettings _clientSettings;
        public readonly Inventory _inventory;
        private readonly Navigation _navigation;

        public Statistics _stats;

        public List<PokemonId> TransferList;
        public List<PokemonId> EvolveList;
        public List<PokemonId> CatchList;
        public List<PokemonId> BerryList;

        public bool running = false;

        public Random rand = new Random();
        public BotInstance(ISettings clientSettings)
        {

            _clientSettings = clientSettings;
            _client = new Client(_clientSettings);
            _inventory = new Inventory(_client);
            _navigation = new Navigation(_client);
            _stats = new Statistics();
        }
        public void Stop()
        {
            running = false;
        }
        public async Task Execute()
        {
            Logger.Write($"Starting Execute on login server: {_clientSettings.AuthType}", LogLevel.Info);
            running = true;
            while (running)
            {
                try
                {
                    if (_clientSettings.AuthType == AuthType.Ptc)
                        await _client.DoPtcLogin(_clientSettings.PtcUsername, _clientSettings.PtcPassword);
                    else if (_clientSettings.AuthType == AuthType.Google)
                        await _client.DoGoogleLogin();

                    await PostLoginExecute();

                }
                catch (AccessTokenExpiredException)
                {
                    Logger.Write($"Access token expired", LogLevel.Info);
                }
                await Task.Delay(rand.Next(8000, 15000));
            }
        }
        public async Task PostLoginExecute()
        {
            while (running)
            {
                try
                {
                    await _client.SetServer();

                    await EvolveAllPokemonWithEnoughCandy();
                    await RecycleItems();
                    await TransferDuplicatePokemon(UserSettings.KeepCP, true);

                    
                    await ExecuteFarmingForts(UserSettings.CatchPokemon);
                    /*await EvolveAllPokemonWithEnoughCandy();
                    await TransferDuplicatePokemon(true);
                    await RecycleItems();
                    await ExecuteFarmingPokestopsAndPokemons();*/
                }
                catch (AccessTokenExpiredException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    //if ()
                    Logger.Write($"Exception: {ex}", LogLevel.Error);
                }

                await Task.Delay(rand.Next(8000, 15000));
            }
            // walk home
            try
            {
                await WalkToStart();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task WalkToStart()
        {
            Logger.Write("Stopping and walking to start point...");
            var update = await _navigation.HumanLikeWalking(new Navigation.Location(_clientSettings.DefaultLatitude, _clientSettings.DefaultLongitude), UserSettings.WalkingSpeed);
            Logger.Write("Bot has been stopped and has reached the start point used. You may now safely exit.");
        }
        public async Task ExecuteFarmingForts(bool getPokes)
        {
            var mapObjects = await _client.GetMapObjects();

            var pokeStops = mapObjects.MapCells.SelectMany(i => i.Forts).Where(i => i.Type == FortType.Checkpoint && i.CooldownCompleteTimestampMs < DateTime.UtcNow.ToUnixTime()).OrderBy(i => LocationUtils.CalculateDistanceInMeters(new Navigation.Location(_client.CurrentLat, _client.CurrentLng), new Navigation.Location(i.Latitude, i.Longitude)));

            foreach (var pokeStop in pokeStops)
            {
                if (!running)
                    break;
                var update =
                    await _navigation.HumanLikeWalking(new Navigation.Location(pokeStop.Latitude, pokeStop.Longitude), UserSettings.WalkingSpeed);

                if (UserSettings.GetForts)
                {
                    //var fortInfo = await client.GetFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);
                    var fortSearch = await _client.SearchFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                    _stats.addExperience(fortSearch.ExperienceAwarded);
                    _stats.updateConsoleTitle(_inventory);

                    await Events.FortFarmed(fortSearch, pokeStop);

                    Logger.Write($"Farmed XP: {fortSearch.ExperienceAwarded}, Gems: { fortSearch.GemsAwarded}, Eggs: {fortSearch.PokemonDataEgg} Items: {StringUtils.GetSummedFriendlyNameOfItemAwardList(fortSearch.ItemsAwarded)}", LogLevel.Info);
                    await Task.Delay(rand.Next(3000, 6000));
                }

                

                if (getPokes)
                   await ExecuteCatchAllNearbyPokemons();

                await Task.Delay(15000);
            }
        }

        private async Task ExecuteCatchAllNearbyPokemons()
        {
            var mapObjects = await _client.GetMapObjects();

            var pokemons = mapObjects.MapCells.SelectMany(i => i.CatchablePokemons).OrderBy(i => LocationUtils.CalculateDistanceInMeters(new Navigation.Location(_client.CurrentLat, _client.CurrentLng), new Navigation.Location(i.Latitude, i.Longitude)));

            foreach (var pokemon in pokemons)
            {
                if (!running)
                    break;

                var update = await _navigation.HumanLikeWalking(new Navigation.Location(pokemon.Latitude, pokemon.Longitude), UserSettings.WalkingSpeed);

                var encounter = await _client.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnpointId);

                var pokeId = encounter?.WildPokemon?.PokemonData.PokemonId;

                
                await CatchEncounter(encounter, pokemon);



                await Task.Delay(rand.Next(15000, 30000));
            }
        }

        
        private async Task CatchEncounter(EncounterResponse encounter, MapPokemon pokemon)
        {

            CatchPokemonResponse caughtPokemonResponse;
            do
            {
                PokemonData pokeData = encounter?.WildPokemon?.PokemonData;

               

                if (encounter?.CaptureProbability.CaptureProbability_.First() < (UserSettings.BerryProbability / 100))
                {
                    if (pokeData != null)
                    {
                        if (BerryList.Contains(pokeData.PokemonId))
                        {
                            await UseBerry(pokemon.EncounterId, pokemon.SpawnpointId);
                        }
                    }
                    
                    
                }

                var pokeball = await GetBestBall(encounter?.WildPokemon);
                
                caughtPokemonResponse = await _client.CatchPokemon(pokemon.EncounterId, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude, pokeball);

                Logger.Write(caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess ? $"We caught a {pokemon.PokemonId} with CP {encounter?.WildPokemon?.PokemonData?.Cp} ({CalculatePokemonPerfection(encounter?.WildPokemon?.PokemonData).ToString("0.00")}% perfection) using a {pokeball}" : $"{pokemon.PokemonId} with CP {encounter?.WildPokemon?.PokemonData?.Cp} got away while using a {pokeball}..", LogLevel.Info);

                if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                {
                    pokeData = encounter?.WildPokemon?.PokemonData;

                    foreach (int xp in caughtPokemonResponse.Scores.Xp)
                        _stats.addExperience(xp);
                    _stats.increasePokemons();
                    _stats.updateConsoleTitle(_inventory);
                    await Events.PokemonCaught(encounter?.WildPokemon?.PokemonData);
                }

                await Task.Delay(rand.Next(1500, 3000));
            }
            while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed);
        }

        public async Task<MiscEnums.Item> GetBestBall(WildPokemon pokemon)
        {
            var pokemonCp = pokemon?.PokemonData?.Cp;

            var pokeBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_POKE_BALL);
            var greatBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_GREAT_BALL);
            var ultraBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_ULTRA_BALL);
            var masterBallsCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_MASTER_BALL);

            if (masterBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_MASTER_BALL;
            else if (ultraBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (ultraBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (greatBallsCount > 0 && pokemonCp >= 350)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (pokeBallsCount > 0)
                return MiscEnums.Item.ITEM_POKE_BALL;
            if (greatBallsCount > 0)
                return MiscEnums.Item.ITEM_GREAT_BALL;
            if (ultraBallsCount > 0)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            if (masterBallsCount > 0)
                return MiscEnums.Item.ITEM_MASTER_BALL;

            return MiscEnums.Item.ITEM_POKE_BALL;
        }

        public async Task UseBerry(ulong encounterId, string spawnPointId)
        {
            if (!UserSettings.UseBerries)
                return;

            var inventoryBalls = await _inventory.GetItems();
            var berries = inventoryBalls.Where(p => (ItemId)p.Item_ == ItemId.ItemRazzBerry);
            var berry = berries.FirstOrDefault();

            if (berry == null)
                return;

            var useRaspberry = await _client.UseCaptureItem(encounterId, AllEnum.ItemId.ItemRazzBerry, spawnPointId);
            Logger.Write($"Use Rasperry. Remaining: {berry.Count}", LogLevel.Info);
            await Task.Delay(rand.Next(4000, 8000));
        }
        public static float CalculatePokemonPerfection(PokemonData poke)
        {
            return ((float)(poke.IndividualAttack * 2 + poke.IndividualDefense + poke.IndividualStamina) / (4.0f * 15.0f)) * 100.0f;
        }
        public async Task EvolveAllPokemonWithEnoughCandy()
        {
            var pokemonToEvolve = await _inventory.GetPokemonToEvolve();
            foreach (var pokemon in pokemonToEvolve)
            {
                if (!EvolveList.Contains(pokemon.PokemonId))
                    continue;

                if (pokemon.Cp < UserSettings.EvolveOverCP && CalculatePokemonPerfection(pokemon) < UserSettings.EvolveOverIV)
                {
                    Logger.Write($"Did not Evolve {pokemon.PokemonId} ({pokemon.Cp} cp, {CalculatePokemonPerfection(pokemon).ToString("0.00")}%) (Under Requirement) ", LogLevel.Info);

                    continue;
                }

                var evolvePokemonOutProto = await _client.EvolvePokemon((ulong)pokemon.Id);
                _stats.increasePokemonsTransfered();
                _stats.updateConsoleTitle(_inventory);
                if (evolvePokemonOutProto.Result == EvolvePokemonOut.Types.EvolvePokemonStatus.PokemonEvolvedSuccess)
                    Logger.Write($"Evolved {pokemon.PokemonId} successfully for {evolvePokemonOutProto.ExpAwarded}xp", LogLevel.Info);
                else
                    Logger.Write($"Failed to evolve {pokemon.PokemonId}. EvolvePokemonOutProto.Result was {evolvePokemonOutProto.Result}, stopping evolving {pokemon.PokemonId}", LogLevel.Info);


                await Task.Delay(rand.Next(3000, 5000));
            }
        }

        public async Task TransferDuplicatePokemon(int keepCp, bool keepPokemonsThatCanEvolve = false)
        {
            var duplicatePokemons = await _inventory.GetDuplicatePokemonToTransfer(keepCp, keepPokemonsThatCanEvolve);

            foreach (var duplicatePokemon in duplicatePokemons)
            {
                // stop transfer of pokemon that are not on transfer list
                if (!TransferList.Contains(duplicatePokemon.PokemonId))
                    continue;

                var transfer = await _client.TransferPokemon(duplicatePokemon.Id);
                _stats.increasePokemonsTransfered();
                _stats.updateConsoleTitle(_inventory);
                Logger.Write($"Transferred {duplicatePokemon.PokemonId} with {duplicatePokemon.Cp} CP", LogLevel.Info);
                await Task.Delay(rand.Next(3000, 6000));
            }
        }

        public async Task TransferPokemonFromList()
        {
            var pokemons = await _inventory.GetPokemons();
            var pokemonList = pokemons as IList<PokemonData> ?? pokemons.ToList();

            foreach (var pokemon in pokemonList)
            {
                if (!TransferList.Contains(pokemon.PokemonId))
                    continue;
                if (pokemon.Cp > UserSettings.KeepCP || CalculatePokemonPerfection(pokemon) > UserSettings.KeepIV)
                {
                    Logger.Write($"Did not Transfer {pokemon.PokemonId} ({pokemon.Cp} cp, {CalculatePokemonPerfection(pokemon).ToString("0.00")}%) (Over Requirement) ", LogLevel.Info);
                    continue;
                }
                var transfer = await _client.TransferPokemon(pokemon.Id);
                _stats.increasePokemonsTransfered();
                _stats.updateConsoleTitle(_inventory);
                Logger.Write($"Transferred {pokemon.PokemonId} with {pokemon.Cp} CP", LogLevel.Info);
                await Task.Delay(rand.Next(3000, 6000));
            }
        }

        public async Task EvolvePokemonFromList()
        {
            var pokemonToEvolve = await _inventory.GetPokemonToEvolve();
            foreach (var pokemon in pokemonToEvolve)
            {
                // skip ones we do not want to evolve
                if (!EvolveList.Contains(pokemon.PokemonId))
                    continue;

                if (pokemon.Cp < UserSettings.EvolveOverCP && CalculatePokemonPerfection(pokemon) < UserSettings.EvolveOverIV)
                {
                    Logger.Write($"Did not Evolve {pokemon.PokemonId} ({pokemon.Cp} cp, {CalculatePokemonPerfection(pokemon).ToString("0.00")}%) (Under Requirement) ", LogLevel.Info);

                    continue;
                }

                var evolvePokemonOutProto = await _client.EvolvePokemon((ulong)pokemon.Id);

                if (evolvePokemonOutProto.Result == EvolvePokemonOut.Types.EvolvePokemonStatus.PokemonEvolvedSuccess)
                    Logger.Write($"Evolved {pokemon.PokemonId} successfully for {evolvePokemonOutProto.ExpAwarded}xp", LogLevel.Info);
                else
                    Logger.Write($"Failed to evolve {pokemon.PokemonId}. EvolvePokemonOutProto.Result was {evolvePokemonOutProto.Result}, stopping evolving {pokemon.PokemonId}", LogLevel.Info);


                await Task.Delay(rand.Next(3500, 5000));
            }
        }

        public async Task RecycleItems()
        {
            var items = await _inventory.GetItemsToRecycle(_clientSettings);

            foreach (var item in items)
            {
                var transfer = await _client.RecycleItem((AllEnum.ItemId)item.Item_, item.Count);
                Logger.Write($"Recycled {item.Count}x {(AllEnum.ItemId)item.Item_}", LogLevel.Info);
                await Task.Delay(rand.Next(4000, 8000));
            }
        }

    }
}
