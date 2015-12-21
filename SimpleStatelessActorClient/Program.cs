using System;
using Microsoft.ServiceFabric.Actors;
using SimpleStatelessActor.Interfaces;

namespace SimpleStatelessActorClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var proxy = ActorProxy.Create<ISimpleStatelessActor>(ActorId.NewId(), new Uri("fabric:/SimpleStatelessActors/SimpleStatelessActorService"));
            Console.WriteLine("Press 1 for Messaging demo and 2 for Events demo:");
            var value = Convert.ToInt32(Console.ReadLine());
            switch (value)
            {
                case 1:
                    MessageDemo(proxy);
                    break;
                case 2:
                    EventsDemo(proxy);
                    break;
            }
            Console.ReadLine();
        }

        private static void EventsDemo(ISimpleStatelessActor proxy)
        {
            var doWork = proxy.DoWorkAsync();
            proxy.SubscribeAsync(new SimpleStatelessActorEventsHandler()).Wait();
            doWork.Wait();
        }

        private static void MessageDemo(ISimpleStatelessActor proxy)
        {
            while (true)
            {
                Console.WriteLine("What's your name?");
                var message = proxy.SayHelloAsync(Console.ReadLine());
                message.Wait();
                Console.WriteLine(message.Result);
                Console.WriteLine(Environment.NewLine);
            }
        }
    }
}