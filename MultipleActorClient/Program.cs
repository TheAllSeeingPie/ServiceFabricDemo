using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using MultipleActors.Interfaces;

namespace MultipleActorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorId = new ActorId("MultipleActorDemo");
            var grumpyProxy = ActorProxy.Create<ITalkingActor>(actorId, new Uri("fabric:/MultipleActors/GrumpyActorService"));
            var happyProxy = ActorProxy.Create<ITalkingActor>(actorId, new Uri("fabric:/MultipleActors/HappyActorService"));

            int value;
            do
            {
                Console.WriteLine("\n\nPress 1 for Synchronicity demo and 2 for Reentrancy demo:");
                value = Convert.ToInt32(Console.ReadLine());
                switch (value)
                {
                    case 1:
                        RunHappyMultipleAsync(happyProxy).Wait();
                        break;
                    case 2:
                        RunHappyGrumpyAsync(grumpyProxy, happyProxy).Wait();
                        break;
                }
            } while (value > 0 && value < 3);
            Console.WriteLine("\n\n----- Press any key to exit! -----");
            Console.ReadLine();
        }

        private static async Task RunHappyMultipleAsync(ITalkingActor happyProxy)
        {
            Console.WriteLine("\n\nDemonstrating Synchronicity by calling the same method asynchronously but showing that they execute one at a time regardless.\n\n");
            var count = 5;
            var tasks = new Task[count];
            for (int i = 0; i < count; i++)
            {
                var id = i;
                Console.WriteLine("{0}:Calling SpeakAsync", id);
                tasks[i] = happyProxy.SpeakAsync().ContinueWith(t=> Console.WriteLine("{0}:{1}", id, t.Result));
            }

            await Task.WhenAll(tasks);
        } 

        private static async Task RunHappyGrumpyAsync(ITalkingActor grumpyProxy, ITalkingActor happyProxy)
        {
            Console.WriteLine("\n\nDemonstrating Reentrancy by showing that you can call methods between Actors without locks as long as you started with a Client call.\n\n");
            var grumpyEvents = grumpyProxy.SubscribeAsync(new ITalkingActorEventHandler());
            var happyEvents = happyProxy.SubscribeAsync(new ITalkingActorEventHandler());

            await Task.Delay(TimeSpan.FromSeconds(5));
            
            Console.WriteLine("Trying to talk to HappyActor");
            var happyTask = happyProxy
                .SpeakAsync()
                .ContinueWith(t=> Console.WriteLine(t.Result));
            Console.WriteLine("Trying to talk to GrumpyActor");
            var grumpyTask = grumpyProxy
                .SpeakAsync()
                .ContinueWith(t => Console.WriteLine(t.Result));

            await Task.WhenAll(grumpyTask, happyTask, grumpyEvents, happyEvents);
        }
    }
}
