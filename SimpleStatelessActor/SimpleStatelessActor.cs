using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using SimpleStatelessActor.Interfaces;

namespace SimpleStatelessActor
{
    /// <remarks>
    /// Each ActorID maps to an instance of this class.
    /// The ISimpleStatelessActor interface (in a separate DLL that client code can
    /// reference) defines the operations exposed by SimpleStatelessActor objects.
    /// </remarks>
    internal class SimpleStatelessActor : StatelessActor, ISimpleStatelessActor
    {
        private Random _random = new Random();

        public async Task DoWorkAsync(int seconds)
        {
            var milliseconds = seconds*1000;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds < milliseconds)
            {
                await Task.Delay(_random.Next(1, 500));
                var @event = GetEvent<ISimpleStatelessActorEvents>();
                @event.Ping("Ping!");
            }
        }

        public Task<string> SayHelloAsync(string name)
        {
            string message = $"Hello {name}";
            return Task.FromResult(message);
        }
    }
}
