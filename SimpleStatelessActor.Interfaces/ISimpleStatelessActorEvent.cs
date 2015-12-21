using Microsoft.ServiceFabric.Actors;

namespace SimpleStatelessActor.Interfaces
{
    public interface ISimpleStatelessActorEvent : IActorEvents
    {
        void Ping(string message);
    }
}