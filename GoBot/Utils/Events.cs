using GoBot.UserLogger;
using POGOProtos.Data;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace GoBot.Utils
{
    public static class Events
    { 

        public static event MessageHandler OnMessageReceived;
        public delegate void MessageHandler(object sender, LogReceivedArgs e);

        public static event PokemonCaughtHandler OnPokemonCaught;
        public static AsyncManualResetEvent PokemonCaughtReset = new AsyncManualResetEvent();
        public delegate void PokemonCaughtHandler(object sender, PokemonCaughtArgs e);

        public static event FortFarmedHandler OnFortFarmed;
        public static AsyncManualResetEvent FortFarmedReset = new AsyncManualResetEvent();
        public delegate void FortFarmedHandler(object sender, FortFarmedArgs e);

        public static event StepWalked OnStepWalked;
        public static AsyncManualResetEvent StepWalkedReset = new AsyncManualResetEvent();
        public delegate void StepWalked(object sender, StepWalkedArgs e);

        public static async void Log(string sender, string message, Color color)
        {
            OnMessageReceived(null, new LogReceivedArgs() { Sender = sender, Message = message, Color = color });
        }

        public static async Task FortFarmed(FortSearchResponse resp, FortData fortData)
        {
            OnFortFarmed(null, new FortFarmedArgs() { SearchResponse = resp, Fort = fortData });

            await FortFarmedReset.WaitAsync();
            FortFarmedReset.Reset();
        }

        public static async Task PokemonCaught(PokemonData poke, ulong pokemonId)
        { 
            OnPokemonCaught(null, new PokemonCaughtArgs() { CaughtPokemon = poke, CaughtID = pokemonId });
            await PokemonCaughtReset.WaitAsync();
            PokemonCaughtReset.Reset();
        }

        public static async Task WaypointStepWalked(Navigation.Location currentLocation, Client client, Navigation nav)
        {
            OnStepWalked(null, new StepWalkedArgs() { curClient = client, curLocation = currentLocation, curNavigation = nav });

            await StepWalkedReset.WaitAsync();
            StepWalkedReset.Reset();
        }
    }
    public class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();

        public Task WaitAsync() { return m_tcs.Task; }

        public void Set()
        {
            var tcs = m_tcs;
            Task.Factory.StartNew(s => ((TaskCompletionSource<bool>)s).TrySetResult(true),
                tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
            tcs.Task.Wait();
        }

        public void Reset()
        {
            while (true)
            {
                var tcs = m_tcs;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref m_tcs, new TaskCompletionSource<bool>(), tcs) == tcs)
                    return;
            }
        }
    }
    public class LogReceivedArgs : EventArgs
    {
        public string Sender;
        public string Message;
        public Color Color;
    }
    public class PokemonCaughtArgs : EventArgs
    {
        public PokemonData CaughtPokemon;
        public ulong CaughtID;
    }
    public class StepWalkedArgs : EventArgs
    {
        public Navigation.Location curLocation;
        public Navigation curNavigation;
        public Client curClient;
    }
    public class FortFarmedArgs : EventArgs
    {
        public FortSearchResponse SearchResponse;
        public FortData Fort;
    }
}
