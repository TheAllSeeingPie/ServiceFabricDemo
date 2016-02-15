using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using SimpleStatefulActor.Interfaces;

namespace SimpleStatefulActorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = ActorProxy.Create<ISimpleStatefulActor>(ActorId.NewId(), new Uri("fabric:/SimpleStatefulActors/SimpleStatefulActorService"));
            Console.WriteLine("Count demo!");
            CountDemo(proxy).Wait();
        }

        private static async Task CountDemo(ISimpleStatefulActor proxy)
        {
            Console.WriteLine("Enter a value for the Count as it is currently: {0}", await proxy.GetCountAsync());
            var count = Convert.ToInt32(Console.ReadLine());
            await proxy.SetCountAsync(count);
            await CountDemo(proxy);
        }
    }
}
