using Microsoft.ServiceFabric.Actors;

namespace MultipleActors.Interfaces
{
    public interface ITalkingActorEvent : IActorEvents
    {
        void EnteredMethod(string methodName, string actorName, ActorId actorId);
    }
}