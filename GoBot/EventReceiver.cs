using GoBot.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoBot.Utils;
using PokemonGo.RocketAPI.GeneratedCode;
using PokemonGo.RocketAPI;

namespace GoBot
{
    public class EventReceiver
    {
        public BotInstance bot;
        public void Set()
        {
            GoBot.Utils.Events.OnPokemonCaught += Events_OnPokemonCaught;
            GoBot.Utils.Events.OnFortFarmed += Events_OnFortFarmed;
        }

        private  async void Events_OnFortFarmed(object sender, Utils.FortFarmedArgs e)
        {
            try
            {
                await bot.EvolvePokemonFromList();
                T.Delay(bot.rand.Next(4500, 7000));
                await bot.RecycleItems();
                T.Delay(bot.rand.Next(4500, 7000));
                Utils.Events.FortFarmedReset.Set();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception: {ex}");
                Utils.Events.FortFarmedReset.Set();
            }
        }
        private async void Events_OnPokemonCaught(object sender, Utils.PokemonCaughtArgs e)
        {
            try
            {
                PokemonData pokeData = e.CaughtPokemon;
                if (pokeData != null)
                {
                    if (!bot.CatchList.Contains(pokeData.PokemonId))
                    {
                        // transfer but wait a bit before
                        T.Delay(bot.rand.Next(9000, 15000));
                        var resp = await bot._client.TransferPokemon(pokeData.Id);
                        bot._stats.increasePokemonsTransfered();
                        bot._stats.updateConsoleTitle(bot._inventory);
                        Logger.Write($"Transferred {pokeData.PokemonId} with {pokeData.Cp} CP (Pokemon was not in Catch list!)", LogLevel.Info);
                    }
                    else
                    {
                        // CHANGED FROM AND TO OR
                        if (pokeData.Cp < UserSettings.CatchOverCP || BotInstance.CalculatePokemonPerfection(pokeData) < UserSettings.CatchOverIV)
                        {
                            T.Delay(bot.rand.Next(9000, 15000));
                            var resp = await bot._client.TransferPokemon(pokeData.Id);
                            // stats
                            bot._stats.increasePokemonsTransfered();
                            bot._stats.updateConsoleTitle(bot._inventory);
                            Logger.Write($"Transferred {pokeData.PokemonId} with {pokeData.Cp} CP ({BotInstance.CalculatePokemonPerfection(pokeData).ToString("0.00")}%) (Under Requirement)", LogLevel.Info);
                        }
                        else
                        {
                            T.Delay(5000);
                          
                        }
                    }
                }
                await bot.TransferDuplicatePokemon(UserSettings.KeepCP, false);
                T.Delay(bot.rand.Next(4500, 7000));
                await bot.RecycleItems();
                T.Delay(bot.rand.Next(4500, 7000));
                Utils.Events.PokemonCaughtReset.Set();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception: {ex}");
                Utils.Events.PokemonCaughtReset.Set();
            }
        }

    

    }

}
