using System;
using Microsoft.ServiceFabric.Actors;
using MultipleActors.Interfaces;

namespace MultipleActorClient
{
    internal class ITalkingActorEventHandler : ITalkingActorEvent
    {
        public void EnteredMethod(string methodName, string actorName, ActorId actorId)
        {
            Console.WriteLine("Entered {0} in actor {1} with actorId {2}", methodName, actorName, actorId);
        }
    }
}