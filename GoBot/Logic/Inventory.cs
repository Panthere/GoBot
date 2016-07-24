using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.GeneratedCode;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using AllEnum;

namespace GoBot.Logic
{
    public class Inventory
    {
        private readonly Client _client;
        private Dictionary<PokemonId, int> EvolutionRequirements = new Dictionary<PokemonId, int>();
        public Inventory(Client client)
        {
            _client = client;

            EvolutionRequirements.Add(PokemonId.Abra, 25);
            EvolutionRequirements.Add(PokemonId.Aerodactyl, -1);
            EvolutionRequirements.Add(PokemonId.Arbok, -1);
            EvolutionRequirements.Add(PokemonId.Arcanine, -1);
            EvolutionRequirements.Add(PokemonId.Articuno, -1);
            EvolutionRequirements.Add(PokemonId.Beedrill, -1);
            EvolutionRequirements.Add(PokemonId.Bellsprout, 25);
            EvolutionRequirements.Add(PokemonId.Blastoise, -1);
            EvolutionRequirements.Add(PokemonId.Bulbasaur, 25);
            EvolutionRequirements.Add(PokemonId.Butterfree, -1);
            EvolutionRequirements.Add(PokemonId.Caterpie, 12);
            EvolutionRequirements.Add(PokemonId.Chansey, -1);
            EvolutionRequirements.Add(PokemonId.Charizard, -1);
            EvolutionRequirements.Add(PokemonId.Charmeleon, 100);
            EvolutionRequirements.Add(PokemonId.Clefable, -1);
            EvolutionRequirements.Add(PokemonId.Cloyster, -1);
            EvolutionRequirements.Add(PokemonId.Cubone, 50);
            EvolutionRequirements.Add(PokemonId.Dewgong, -1);
            EvolutionRequirements.Add(PokemonId.Diglett, 50);
            EvolutionRequirements.Add(PokemonId.Ditto, -1);
            EvolutionRequirements.Add(PokemonId.Dodrio, -1);
            EvolutionRequirements.Add(PokemonId.Doduo, 50);
            EvolutionRequirements.Add(PokemonId.Dragonair, 100);
            EvolutionRequirements.Add(PokemonId.Dragonite, -1);
            EvolutionRequirements.Add(PokemonId.Dratini, 25);
            EvolutionRequirements.Add(PokemonId.Drowzee, 50);
            EvolutionRequirements.Add(PokemonId.Dugtrio, -1);
            EvolutionRequirements.Add(PokemonId.Eevee, 25);
            EvolutionRequirements.Add(PokemonId.Ekans, 50);
            EvolutionRequirements.Add(PokemonId.Electabuzz, -1);
            EvolutionRequirements.Add(PokemonId.Electrode, -1);
            EvolutionRequirements.Add(PokemonId.Exeggcute, 50);
            EvolutionRequirements.Add(PokemonId.Exeggutor, -1);
            EvolutionRequirements.Add(PokemonId.Farfetchd, -1);
            EvolutionRequirements.Add(PokemonId.Fearow, -1);
            EvolutionRequirements.Add(PokemonId.Flareon, -1);
            EvolutionRequirements.Add(PokemonId.Gastly, 25);
            EvolutionRequirements.Add(PokemonId.Gengar, -1);
            EvolutionRequirements.Add(PokemonId.Gloom, 100);
            EvolutionRequirements.Add(PokemonId.Golbat, -1);
            EvolutionRequirements.Add(PokemonId.Goldeen, 50);
            EvolutionRequirements.Add(PokemonId.Golduck, -1);
            EvolutionRequirements.Add(PokemonId.Golem, -1);
            EvolutionRequirements.Add(PokemonId.Graveler, 100);
            EvolutionRequirements.Add(PokemonId.Grimer, -1);
            EvolutionRequirements.Add(PokemonId.Growlithe, 50);
            EvolutionRequirements.Add(PokemonId.Gyarados, -1);
            EvolutionRequirements.Add(PokemonId.Haunter, 100);
            EvolutionRequirements.Add(PokemonId.Hitmonchan, -1);
            EvolutionRequirements.Add(PokemonId.Hitmonlee, -1);
            EvolutionRequirements.Add(PokemonId.Horsea , 50);
            EvolutionRequirements.Add(PokemonId.Hypno , -1);
            EvolutionRequirements.Add(PokemonId.Ivysaur, 100);
            EvolutionRequirements.Add(PokemonId.Jigglypuff, 50);
            EvolutionRequirements.Add(PokemonId.Jolteon, -1);
            EvolutionRequirements.Add(PokemonId.Jynx, -1);
            EvolutionRequirements.Add(PokemonId.Kabuto, 50);
            EvolutionRequirements.Add(PokemonId.Kabutops, -1);
            EvolutionRequirements.Add(PokemonId.Kadabra, 100);
            EvolutionRequirements.Add(PokemonId.Kakuna, 50);
            EvolutionRequirements.Add(PokemonId.Kangaskhan, -1);
            EvolutionRequirements.Add(PokemonId.Kingler, -1);
            EvolutionRequirements.Add(PokemonId.Koffing, 50);
            EvolutionRequirements.Add(PokemonId.Krabby, 50);
            EvolutionRequirements.Add(PokemonId.Lapras, -1);
            EvolutionRequirements.Add(PokemonId.Lickitung, -1);
            EvolutionRequirements.Add(PokemonId.Machamp, -1);
            EvolutionRequirements.Add(PokemonId.Machoke , 100);
            EvolutionRequirements.Add(PokemonId.Machop, 25);
            EvolutionRequirements.Add(PokemonId.Magikarp, 400);
            EvolutionRequirements.Add(PokemonId.Magmar, -1);
            EvolutionRequirements.Add(PokemonId.Magnemite, 50);
            EvolutionRequirements.Add(PokemonId.Magneton, -1);
            EvolutionRequirements.Add(PokemonId.Mankey, 50);
            EvolutionRequirements.Add(PokemonId.Marowak, -1);
            EvolutionRequirements.Add(PokemonId.Meowth, 50);
            EvolutionRequirements.Add(PokemonId.Metapod, 50);
            EvolutionRequirements.Add(PokemonId.Mew , -1);
            EvolutionRequirements.Add(PokemonId.Mewtwo, -1);
            EvolutionRequirements.Add(PokemonId.Missingno, -1);
            EvolutionRequirements.Add(PokemonId.Moltres, -1);
            EvolutionRequirements.Add(PokemonId.MrMime, -1);
            EvolutionRequirements.Add(PokemonId.Muk, -1);
            EvolutionRequirements.Add(PokemonId.Nidoking, -1);
            EvolutionRequirements.Add(PokemonId.Nidoqueen, -1);
            EvolutionRequirements.Add(PokemonId.NidoranFemale, 25);
            EvolutionRequirements.Add(PokemonId.NidoranMale, 25);
            EvolutionRequirements.Add(PokemonId.Nidorina, 100);
            EvolutionRequirements.Add(PokemonId.Nidorino, 100);
            EvolutionRequirements.Add(PokemonId.Ninetales, -1);
            EvolutionRequirements.Add(PokemonId.Oddish, 25);
            EvolutionRequirements.Add(PokemonId.Omanyte, 50);
            EvolutionRequirements.Add(PokemonId.Omastar, -1);
            EvolutionRequirements.Add(PokemonId.Onix, -1);
            EvolutionRequirements.Add(PokemonId.Paras, 50);
            EvolutionRequirements.Add(PokemonId.Parasect, -1);
            EvolutionRequirements.Add(PokemonId.Persian, -1);
            EvolutionRequirements.Add(PokemonId.Pidgeot, -1);
            EvolutionRequirements.Add(PokemonId.Pidgeotto, 50);
            EvolutionRequirements.Add(PokemonId.Pidgey, 12);
            EvolutionRequirements.Add(PokemonId.Pikachu, 50);
            EvolutionRequirements.Add(PokemonId.Pinsir, -1);
            EvolutionRequirements.Add(PokemonId.Poliwag, 25);
            EvolutionRequirements.Add(PokemonId.Poliwhirl, 50);
            EvolutionRequirements.Add(PokemonId.Poliwrath, -1);
            EvolutionRequirements.Add(PokemonId.Ponyta, 50);
            EvolutionRequirements.Add(PokemonId.Porygon, -1);
            EvolutionRequirements.Add(PokemonId.Primeape, -1);
            EvolutionRequirements.Add(PokemonId.Psyduck, 50);
            EvolutionRequirements.Add(PokemonId.Raichu, -1);
            EvolutionRequirements.Add(PokemonId.Rapidash, -1);
            EvolutionRequirements.Add(PokemonId.Raticate, -1);
            EvolutionRequirements.Add(PokemonId.Rattata, 25);
            EvolutionRequirements.Add(PokemonId.Rhydon, -1);
            EvolutionRequirements.Add(PokemonId.Rhyhorn, 50);
            EvolutionRequirements.Add(PokemonId.Sandshrew, 50);
            EvolutionRequirements.Add(PokemonId.Scyther, -1);
            EvolutionRequirements.Add(PokemonId.Seadra , -1);
            EvolutionRequirements.Add(PokemonId.Seaking, -1);
            EvolutionRequirements.Add(PokemonId.Seel, 50);
            EvolutionRequirements.Add(PokemonId.Shellder, 50);
            EvolutionRequirements.Add(PokemonId.Slowbro, -1);
            EvolutionRequirements.Add(PokemonId.Slowpoke, 50);
            EvolutionRequirements.Add(PokemonId.Snorlax, -1);
            EvolutionRequirements.Add(PokemonId.Spearow , 50);
            EvolutionRequirements.Add(PokemonId.Squirtle, 25);
            EvolutionRequirements.Add(PokemonId.Starmie, -1);
            EvolutionRequirements.Add(PokemonId.Staryu, 50);
            EvolutionRequirements.Add(PokemonId.Tangela, -1);
            EvolutionRequirements.Add(PokemonId.Tauros, -1);
            EvolutionRequirements.Add(PokemonId.Tentacool, 50);
            EvolutionRequirements.Add(PokemonId.Tentacruel, -1);
            EvolutionRequirements.Add(PokemonId.Vaporeon, -1);
            EvolutionRequirements.Add(PokemonId.Venomoth, -1);
            EvolutionRequirements.Add(PokemonId.Venonat, 50);
            EvolutionRequirements.Add(PokemonId.Venusaur, -1);
            EvolutionRequirements.Add(PokemonId.Vileplume, -1);
            EvolutionRequirements.Add(PokemonId.Voltorb, 50);
            EvolutionRequirements.Add(PokemonId.Vulpix, 50);
            EvolutionRequirements.Add(PokemonId.Wartortle, 100);
            EvolutionRequirements.Add(PokemonId.Weedle, 12);
            EvolutionRequirements.Add(PokemonId.Weepinbell, 100);
            EvolutionRequirements.Add(PokemonId.Weezing, -1);
            EvolutionRequirements.Add(PokemonId.Wigglytuff, -1);
            EvolutionRequirements.Add(PokemonId.Zapdos, -1);
            EvolutionRequirements.Add(PokemonId.Zubat, 50);
        }



        // candy requirements

        public async Task<IEnumerable<PokemonData>> GetHighestsCP(int limit)
        {
            var myPokemon = await GetPokemons();
            var pokemons = myPokemon.ToList();
            return pokemons.OrderByDescending(x => x.Cp).ThenBy(n => n.StaminaMax).Take(limit);
        }

        public async Task<IEnumerable<PokemonData>> GetHighestsPerfect(int limit)
        {
            var myPokemon = await GetPokemons();
            var pokemons = myPokemon.ToList();
            return pokemons.OrderByDescending(BotInstance.CalculatePokemonPerfection).Take(limit);
        }

        public async Task<PokemonData> GetHighestPokemonOfTypeByCP(PokemonData pokemon)
        {
            var myPokemon = await GetPokemons();
            var pokemons = myPokemon.ToList();
            return pokemons.Where(x => x.PokemonId == pokemon.PokemonId)
                .OrderByDescending(x => x.Cp)
                .First();
        }

        public async Task<IEnumerable<PlayerStats>> GetPlayerStats()
        {
            var inventory = await _client.GetInventory();
            return inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.PlayerStats)
                .Where(p => p != null);
        }

        public async Task<PokemonData> GetLastCaughtPokemon(PokemonData match)
        {
            var inventory = await _client.GetInventory();
            return
                inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.Pokemon)
                    .Where(p => p != null && p?.PokemonId > 0
                    && p?.Cp == match.Cp
                    && p?.PokemonId == match.PokemonId
                    && p?.WeightKg == match.WeightKg 
                    && p?.Stamina == match.Stamina
                    && BotInstance.CalculatePokemonPerfection(p) == BotInstance.CalculatePokemonPerfection(match)).First();
        }

        public async Task<IEnumerable<PokemonData>> GetPokemons()
        {
            var inventory = await _client.GetInventory();
            return
                inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.Pokemon)
                    .Where(p => p != null && p?.PokemonId > 0);
        }

        public async Task<IEnumerable<PokemonFamily>> GetPokemonFamilies()
        {
            var inventory = await _client.GetInventory();
            return
                inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PokemonFamily)
                    .Where(p => p != null && p?.FamilyId != PokemonFamilyId.FamilyUnset);
        }

        public async Task<IEnumerable<PokemonSettings>> GetPokemonSettings()
        {
            var templates = await _client.GetItemTemplates();
            return
                templates.ItemTemplates.Select(i => i.PokemonSettings)
                    .Where(p => p != null && p?.FamilyId != PokemonFamilyId.FamilyUnset);
        }

        public async Task<int[]> GetRequiredCandy(List<PokemonSettings> pokemonSettings, PokemonFamily[] pokemonFamilies, PokemonId pokemon)
        {
           
            var settings = pokemonSettings.Single(x => x.PokemonId == pokemon);
            var familyCandy = pokemonFamilies.Single(x => settings.FamilyId == x.FamilyId);

            return new[] { familyCandy.Candy, settings.CandyToEvolve };
        }

        public async Task<IEnumerable<PokemonData>> GetDuplicatePokemonToTransfer(int belowCp, bool keepPokemonsThatCanEvolve = false)
        {
            var myPokemon = await GetPokemons();

            var pokemonList = myPokemon as IList<PokemonData> ?? myPokemon.ToList();
            if (keepPokemonsThatCanEvolve)
            {
                var results = new List<PokemonData>();
                var pokemonsThatCanBeTransfered = pokemonList.GroupBy(p => p.PokemonId)
                    .Where(x => x.Count() > 1).ToList();

                var myPokemonSettings = await GetPokemonSettings();
                var pokemonSettings = myPokemonSettings.ToList();

                var myPokemonFamilies = await GetPokemonFamilies();
                var pokemonFamilies = myPokemonFamilies.ToArray();

                foreach (var pokemon in pokemonsThatCanBeTransfered)
                {
                    
                    var settings = pokemonSettings.Single(x => x.PokemonId == pokemon.Key);
                    var familyCandy = pokemonFamilies.Single(x => settings.FamilyId == x.FamilyId);

                    if (settings.CandyToEvolve == 0)
                        continue;

                    var amountToSkip = (familyCandy.Candy + settings.CandyToEvolve - 1) / settings.CandyToEvolve;

                    results.AddRange(pokemonList.Where(x => x.PokemonId == pokemon.Key && x.Favorite == 0 && x.Cp < belowCp)
                        .OrderByDescending(x => x.Cp)
                        .ThenBy(n => n.StaminaMax)
                        .Skip(amountToSkip).Skip(UserSettings.TopX)
                        .ToList());

                }

                return results;
            }

            return pokemonList
                .GroupBy(p => p.PokemonId)
                .Where(x => x.Count() > 1)
                .SelectMany(p => p.Where(x => x.Favorite == 0 && x.Cp < belowCp).OrderByDescending(x => x.Cp).ThenBy(n => n.StaminaMax).Skip(1).Skip(UserSettings.TopX).ToList());
        }


        public async Task<IEnumerable<PokemonData>> GetPokemonToEvolve(params PokemonId[] filters)
        {
            var myPokemons = await GetPokemons();
            var pokemons = myPokemons.ToList();

            var myPokemonSettings = await GetPokemonSettings();
            var pokemonSettings = myPokemonSettings.ToList();

            var myPokemonFamilies = await GetPokemonFamilies();
            var pokemonFamilies = myPokemonFamilies.ToArray();

            var pokemonToEvolve = new List<PokemonData>();
            foreach (var pokemon in pokemons)
            {
                var settings = pokemonSettings.Single(x => x.PokemonId == pokemon.PokemonId);
                var familyCandy = pokemonFamilies.Single(x => settings.FamilyId == x.FamilyId);
                
                //Don't evolve if we can't evolve it
                if (settings.EvolutionIds.Count == 0)
                    continue;

                var pokemonCandyNeededAlready = pokemonToEvolve.Count(p => pokemonSettings.Single(x => x.PokemonId == p.PokemonId).FamilyId == settings.FamilyId) * settings.CandyToEvolve;
                if (familyCandy.Candy - pokemonCandyNeededAlready > settings.CandyToEvolve)
                    pokemonToEvolve.Add(pokemon);
            }

            return pokemonToEvolve;
        }



        public async Task<IEnumerable<Item>> GetItems()
        {
            var inventory = await _client.GetInventory();
            return inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.Item)
                .Where(p => p != null);
        }

        public async Task<int> GetItemAmountByType(MiscEnums.Item type)
        {
            var pokeballs = await GetItems();
            return pokeballs.FirstOrDefault(i => (MiscEnums.Item)i.Item_ == type)?.Count ?? 0;
        }

        public async Task<IEnumerable<Item>> GetItemsToRecycle(ISettings settings)
        {
            var myItems = await GetItems();

            return myItems
                .Where(x => UserSettings.ItemRecycleFilter.Any(f => f.Key == ((ItemId)x.Item_) && x.Count > f.Value))
                .Select(x => new Item { Item_ = x.Item_, Count = x.Count - UserSettings.ItemRecycleFilter.Single(f => f.Key == (ItemId)x.Item_).Value, Unseen = x.Unseen });
        }
    }
}