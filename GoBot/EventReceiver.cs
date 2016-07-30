using GoBot.Logic;
using GoBot.UserLogger;
using GoBot.Utils;
using POGOProtos.Data;
using System;

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
                await bot.TransferDuplicatePokemon(UserSettings.KeepCP, false);
                await T.Delay(bot.rand.Next(4500, 7000));
                await bot.EvolvePokemonFromList();
                await T.Delay(bot.rand.Next(4500, 7000));
                await bot.RecycleItems();
                await T.Delay(bot.rand.Next(4500, 7000));
                Utils.Events.FortFarmedReset.Set();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception: {ex}", LogLevel.Error, ConsoleColor.Red);
                Utils.Events.FortFarmedReset.Set();
            }
        }
        private async void Events_OnPokemonCaught(object sender, Utils.PokemonCaughtArgs e)
        {
            try
            {
                // Id is zero because it has not been caught 'yet' as it is a 'wild' encounter
                PokemonData pokeData = e.CaughtPokemon;
                if (pokeData != null)
                {
                    if (!bot.CatchList.Contains(pokeData.PokemonId))
                    {
                        // transfer but wait a bit before
                        await T.Delay(bot.rand.Next(9000, 15000));
                        //var actualPokemon = await bot._inventory.GetLastCaughtPokemon(pokeData);

                      
                        var resp = await bot._client.Inventory.TransferPokemon(e.CaughtID);
                        // stats
                        if (resp.Result == POGOProtos.Networking.Responses.ReleasePokemonResponse.Types.Result.Success)
                        {
                            bot._stats.increasePokemonsTransfered();
                            bot._stats.updateConsoleTitle(bot._inventory);
                            Logger.Write($"Transferred {pokeData.PokemonId} with {pokeData.Cp} CP (Pokemon was not in Catch list!)", LogLevel.Info, ConsoleColor.DarkGreen);
                        }
                      
                       
                    }
                    else
                    {
                        // CHANGED FROM AND TO OR
                        if (pokeData.Cp < UserSettings.CatchOverCP || BotInstance.CalculatePokemonPerfection(pokeData) < UserSettings.CatchOverIV)
                        {
                            await T.Delay(bot.rand.Next(9000, 15000));
                            var actualPokemon = await bot._inventory.GetPokemonById(e.CaughtID);//await bot._inventory.GetLastCaughtPokemon(pokeData);
                            if (actualPokemon != null)
                            {
                                var highest = await bot._inventory.GetHighestPokemonOfTypeByCP(actualPokemon);

                                // if it's not the highest
                                if (highest.Id != actualPokemon.Id)
                                {

                                    var resp = await bot._client.Inventory.TransferPokemon(actualPokemon.Id);
                                    // stats
                                    if (resp.Result == POGOProtos.Networking.Responses.ReleasePokemonResponse.Types.Result.Success)
                                    {
                                        bot._stats.increasePokemonsTransfered();
                                        bot._stats.updateConsoleTitle(bot._inventory);
                                        Logger.Write($"Transferred {pokeData.PokemonId} with {pokeData.Cp} CP ({BotInstance.CalculatePokemonPerfection(pokeData).ToString("0.00")}%) (Under Requirement)", LogLevel.Info, ConsoleColor.DarkGreen);
                                    }
                                }
                                else
                                {

                                    Logger.Write($"Did NOT transfer {pokeData.PokemonId} with {pokeData.Cp} CP ({BotInstance.CalculatePokemonPerfection(pokeData).ToString("0.00")}%) (Highest in Group)", LogLevel.Info, ConsoleColor.DarkGreen);

                                }
                            }
                            else
                            {
                                Logger.Write("The pokemon was not found in our inventory? If you're getting this, then something is very wrong.");
                            }
                            
                        }
                        else
                        {
                            await T.Delay(5000);
                          
                        }
                    }
                }
                await bot.TransferDuplicatePokemon(UserSettings.KeepCP, false);
                await T.Delay(bot.rand.Next(4500, 7000));
                await bot.RecycleItems();
                await T.Delay(bot.rand.Next(4500, 7000));
                Utils.Events.PokemonCaughtReset.Set();
            }
            catch (Exception ex)
            {
                Logger.Write($"Exception: {ex}", LogLevel.Error, ConsoleColor.Red);
                Utils.Events.PokemonCaughtReset.Set();
            }
        }

    

    }

}
