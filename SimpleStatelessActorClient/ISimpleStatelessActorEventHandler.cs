using System;
using SimpleStatelessActor.Interfaces;

namespace SimpleStatelessActorClient
{
    internal class ISimpleStatelessActorEventHandler : ISimpleStatelessActorEvent
    {
        public void Ping(string message)
        {
            Console.Write($"{message}\t");
        }
    }
}