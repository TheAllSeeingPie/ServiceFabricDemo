using System;
using SimpleStatelessActor.Interfaces;

namespace SimpleStatelessActorClient
{
    internal class SimpleStatelessActorEventsHandler : ISimpleStatelessActorEvents
    {
        public void Ping(string message)
        {
            Console.Write($"{message}\t");
        }
    }
}