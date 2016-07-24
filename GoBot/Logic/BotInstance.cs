
using AllEnum;
using GoBot.UserLogger;
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
            Logger.Write("Stop was called!");
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
                    running = true;
                    await PostLoginExecute();
                    running = true;
                }
                catch (AccessTokenExpiredException)
                {
                    Logger.Write($"Access token expired", LogLevel.Info);
                }
                Logger.Write($"Looping Execute Again", LogLevel.Info);
                T.Delay(rand.Next(8000, 15000));
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
                    Logger.Write("Access token was expired");
                    throw;
                }
                catch (Exception ex)
                {
                    //if ()
                    Logger.Write($"Exception (postinvoke): {ex}", LogLevel.Error);
                }

                T.Delay(rand.Next(8000, 15000));
                Logger.Write($"Looping PostLogin Again", LogLevel.Info);
            }
            Logger.Write($"We're out of the loop now, Running is {running}");
            // walk home
            try
            {
                await WalkToStart();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception: {ex}", LogLevel.Error);
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
                if (UserSettings.Teleport)
                {
                    // You'll be banned, I warned you...
                    var update = await _client.UpdatePlayerLocation(pokeStop.Latitude, pokeStop.Longitude, UserSettings.Altitude);
                }
                else
                {
                    var update =
                        await _navigation.HumanLikeWalking(new Navigation.Location(pokeStop.Latitude, pokeStop.Longitude), UserSettings.WalkingSpeed);
                }

                if (UserSettings.GetForts)
                {
                    //var fortInfo = await client.GetFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);
                    var fortSearch = await _client.SearchFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                    _stats.addExperience(fortSearch.ExperienceAwarded);
                    _stats.updateConsoleTitle(_inventory);

                    await Events.FortFarmed(fortSearch, pokeStop);

                    Logger.Write($"Farmed XP: {fortSearch.ExperienceAwarded}, Gems: { fortSearch.GemsAwarded}, Eggs: {fortSearch.PokemonDataEgg} Items: {StringUtils.GetSummedFriendlyNameOfItemAwardList(fortSearch.ItemsAwarded)}", LogLevel.Info);
                    T.Delay(rand.Next(3000, 6000));
                }

                var profile = await _client.GetProfile();
                _stats.getStardust(profile.Profile.Currency.ToArray()[1].Amount);
                _stats.updateConsoleTitle(_inventory);

                if (getPokes)
                   await ExecuteCatchAllNearbyPokemons();

                T.Delay(15000);
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
                try
                {
                    if (UserSettings.Teleport)
                    {
                        var update = await _client.UpdatePlayerLocation(pokemon.Latitude, pokemon.Longitude, UserSettings.Altitude);
                    }
                    else
                    {
                        var update = await _navigation.HumanLikeWalking(new Navigation.Location(pokemon.Latitude, pokemon.Longitude), UserSettings.WalkingSpeed);
                    }

                    var encounter = await _client.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnpointId);

                    var pokeId = encounter?.WildPokemon?.PokemonData.PokemonId;


                    await CatchEncounter(encounter, pokemon);



                    T.Delay(rand.Next(15000, 30000));
                }
                catch (Exception ex)
                {
                    Logger.Write($"Exception CatchNearby: {ex}");
                }
            }
        }

        
        private async Task CatchEncounter(EncounterResponse encounter, MapPokemon pokemon)
        {

            CatchPokemonResponse caughtPokemonResponse;
            do
            {
                try
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

                    if (pokeball == MiscEnums.Item.ITEM_UNKNOWN)
                    {
                        Logger.Write("No Pokeballs to use! STOPPING BOT!");
                        Stop();
                        return;
                    }
                    caughtPokemonResponse = await _client.CatchPokemon(pokemon.EncounterId, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude, pokeball);

                    Logger.Write(caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess ? $"We caught a {pokemon.PokemonId} with CP {encounter?.WildPokemon?.PokemonData?.Cp} ({CalculatePokemonPerfection(encounter?.WildPokemon?.PokemonData).ToString("0.00")}% perfection) using a {pokeball}" : $"{pokemon.PokemonId} with CP {encounter?.WildPokemon?.PokemonData?.Cp} got away while using a {pokeball}..", LogLevel.Info);

                    if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                    {
                        pokeData = encounter?.WildPokemon?.PokemonData;

                        foreach (int xp in caughtPokemonResponse.Scores.Xp)
                            _stats.addExperience(xp);

                        var profile = await _client.GetProfile();
                        _stats.getStardust(profile.Profile.Currency.ToArray()[1].Amount);

                        _stats.increasePokemons();
                        _stats.updateConsoleTitle(_inventory);
                        await Events.PokemonCaught(encounter?.WildPokemon?.PokemonData);
                    }

                    T.Delay(rand.Next(1500, 3000));
                }
                catch (Exception ex)
                {
                    Logger.Write($"Exception in Encounter: {ex}");
                    break;
                }
            }
            while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed);
        }

        private async Task<MiscEnums.Item> GetBestBall(WildPokemon pokemon)
        {
            var pokemonCp = pokemon?.PokemonData?.Cp;

            var items = await _inventory.GetItems();
            var balls = items.Where(i => ((MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_POKE_BALL
                                      || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_GREAT_BALL
                                      || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_ULTRA_BALL
                                      || (MiscEnums.Item)i.Item_ == MiscEnums.Item.ITEM_MASTER_BALL) && i.Count > 0).GroupBy(i => ((MiscEnums.Item)i.Item_)).ToList();
            if (balls.Count == 0) return MiscEnums.Item.ITEM_UNKNOWN;

            var pokeBalls = balls.Any(g => g.Key == MiscEnums.Item.ITEM_POKE_BALL);
            var greatBalls = balls.Any(g => g.Key == MiscEnums.Item.ITEM_GREAT_BALL);
            var ultraBalls = balls.Any(g => g.Key == MiscEnums.Item.ITEM_ULTRA_BALL);
            var masterBalls = balls.Any(g => g.Key == MiscEnums.Item.ITEM_MASTER_BALL);

            if (masterBalls && pokemonCp >= 2000)
                return MiscEnums.Item.ITEM_MASTER_BALL;
            else if (ultraBalls && pokemonCp >= 2000)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBalls && pokemonCp >= 2000)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (ultraBalls && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBalls && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (greatBalls && pokemonCp >= 500)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            return balls.OrderBy(g => g.Key).First().Key;
        }
        private async Task<ItemId> GetBestBerry(WildPokemon pokemon)
        {
            var pokemonCp = pokemon?.PokemonData?.Cp;

            var items = await _inventory.GetItems();
            var berries = items.Where(i => (ItemId)i.Item_ == ItemId.ItemRazzBerry
                                        || (ItemId)i.Item_ == ItemId.ItemBlukBerry
                                        || (ItemId)i.Item_ == ItemId.ItemNanabBerry
                                        || (ItemId)i.Item_ == ItemId.ItemWeparBerry
                                        || (ItemId)i.Item_ == ItemId.ItemPinapBerry).GroupBy(i => ((ItemId)i.Item_)).ToList();
            if (berries.Count == 0 || pokemonCp <= 350) return ItemId.ItemUnknown;

            var razzBerryCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_RAZZ_BERRY);
            var blukBerryCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_BLUK_BERRY);
            var nanabBerryCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_NANAB_BERRY);
            var weparBerryCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_WEPAR_BERRY);
            var pinapBerryCount = await _inventory.GetItemAmountByType(MiscEnums.Item.ITEM_PINAP_BERRY);

            if (pinapBerryCount > 0 && pokemonCp >= 2000)
                return ItemId.ItemPinapBerry;
            else if (weparBerryCount > 0 && pokemonCp >= 2000)
                return ItemId.ItemWeparBerry;
            else if (nanabBerryCount > 0 && pokemonCp >= 2000)
                return ItemId.ItemNanabBerry;
            else if (nanabBerryCount > 0 && pokemonCp >= 2000)
                return ItemId.ItemBlukBerry;

            if (weparBerryCount > 0 && pokemonCp >= 1500)
                return ItemId.ItemWeparBerry;
            else if (nanabBerryCount > 0 && pokemonCp >= 1500)
                return ItemId.ItemNanabBerry;
            else if (blukBerryCount > 0 && pokemonCp >= 1500)
                return ItemId.ItemBlukBerry;

            if (nanabBerryCount > 0 && pokemonCp >= 1000)
                return ItemId.ItemNanabBerry;
            else if (blukBerryCount > 0 && pokemonCp >= 1000)
                return ItemId.ItemBlukBerry;

            if (blukBerryCount > 0 && pokemonCp >= 500)
                return ItemId.ItemBlukBerry;

            return berries.OrderBy(g => g.Key).First().Key;
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

            var useRaspberry = await _client.UseCaptureItem(encounterId, ItemId.ItemRazzBerry, spawnPointId);
            Logger.Write($"Use Rasperry. Remaining: {berry.Count}", LogLevel.Info);
            T.Delay(rand.Next(4000, 8000));
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


                T.Delay(rand.Next(3000, 5000));
            }
        }

        public async Task TransferDuplicatePokemon(int keepCp, bool keepPokemonsThatCanEvolve = false)
        {

            var duplicatePokemons = await _inventory.GetDuplicatePokemonToTransfer(keepCp, keepPokemonsThatCanEvolve);

            Logger.Write($"Sorting through {duplicatePokemons.ToList().Count} pokemon to transfer duplicates...");

            foreach (var duplicatePokemon in duplicatePokemons)
            {
                // stop transfer of pokemon that are not on transfer list
                if (!TransferList.Contains(duplicatePokemon.PokemonId))
                    continue;

                if (duplicatePokemon.Cp > UserSettings.KeepCP || CalculatePokemonPerfection(duplicatePokemon) > UserSettings.KeepIV)
                {
                    Logger.Write($"Did not Transfer {duplicatePokemon.PokemonId} ({duplicatePokemon.Cp} cp, {CalculatePokemonPerfection(duplicatePokemon).ToString("0.00")}%) (Over Requirement) ", LogLevel.Info);
                    continue;
                }

                var transfer = await _client.TransferPokemon(duplicatePokemon.Id);
                _stats.increasePokemonsTransfered();
                _stats.updateConsoleTitle(_inventory);
                Logger.Write($"Transferred {duplicatePokemon.PokemonId} with {duplicatePokemon.Cp} CP", LogLevel.Info);
                T.Delay(rand.Next(3000, 6000));
            }
        }

        public async Task TransferPokemonFromList()
        {
            var pokemons = await _inventory.GetPokemons();
            var pokemonList = pokemons as IList<PokemonData> ?? pokemons.ToList();

            // UNTESTED
            foreach (var pokemon in pokemonList.Where(x => x.Favorite == 0).OrderByDescending(i=> i.Cp).ThenBy(i => i.StaminaMax).Skip(UserSettings.TopX))
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
                T.Delay(rand.Next(3000, 6000));
            }
        }

        public async Task OverrideTransfer(int keepCp, int keepIv)
        {
            var pokemons = await _inventory.GetPokemons();
            var pokemonList = pokemons as IList<PokemonData> ?? pokemons.ToList();
            Logger.Write($"Forcibly (override) sorting transfer of {pokemonList.Count} pokemon(s)");
            // UNTESTED
            foreach (var pokemon in pokemonList.Where(x => x.Favorite == 0).OrderByDescending(i => i.Cp).ThenBy(i => i.StaminaMax).Skip(UserSettings.TopX))
            {
               // if (!TransferList.Contains(pokemon.PokemonId))
               //     continue;
                if (pokemon.Cp > keepCp || CalculatePokemonPerfection(pokemon) > keepIv)
                {
                    Logger.Write($"Did not Transfer {pokemon.PokemonId} ({pokemon.Cp} cp, {CalculatePokemonPerfection(pokemon).ToString("0.00")}%) (Over Requirement) ", LogLevel.Info);
                    continue;
                }
                var transfer = await _client.TransferPokemon(pokemon.Id);
                _stats.increasePokemonsTransfered();
                _stats.updateConsoleTitle(_inventory);
                Logger.Write($"Transferred {pokemon.PokemonId} with {pokemon.Cp} CP ({CalculatePokemonPerfection(pokemon).ToString("0.00")}%)", LogLevel.Info);
                T.Delay(rand.Next(3000, 6000));
            }
        }

        public async Task OverrideEvolve(int keepCp, int keepIv)
        {
            var pokemonToEvolve = await _inventory.GetPokemonToEvolve();
            foreach (var pokemon in pokemonToEvolve)
            {
                //if (!EvolveList.Contains(pokemon.PokemonId))
                //    continue;

                if (pokemon.Cp < keepCp || CalculatePokemonPerfection(pokemon) < keepIv)
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


                T.Delay(rand.Next(3000, 5000));
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


                T.Delay(rand.Next(3500, 5000));
            }
        }

        public async Task RecycleItems()
        {
            var items = await _inventory.GetItemsToRecycle(_clientSettings);

            foreach (var item in items)
            {
                var transfer = await _client.RecycleItem((ItemId)item.Item_, item.Count);
                Logger.Write($"Recycled {item.Count}x {(ItemId)item.Item_}", LogLevel.Info);
                T.Delay(rand.Next(4000, 8000));
            }
        }

    }
}
