using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using SimpleStatefulService;

namespace SimpleStatefulServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Using the default partition by passing 0
            var proxy = ServiceProxy.Create<ISimpleStatefulService>(0, new Uri("fabric:/SimpleStatefulServices/SimpleStatefulService"));
            Console.WriteLine("Services ReliableQueues demo!");
            QueueingDemo(proxy).Wait();
        }

        private static async Task QueueingDemo(ISimpleStatefulService proxy)
        {
            Console.WriteLine("Type any value to add to collection, hit enter to view and empty queue:");
            var data = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(data))
            {
                var strings = await proxy.GetStrings();
                Console.WriteLine("Contents of the queue:");
                foreach (var s in strings)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                await proxy.SetString(data);
            }
            await QueueingDemo(proxy);
        }
    }
}
