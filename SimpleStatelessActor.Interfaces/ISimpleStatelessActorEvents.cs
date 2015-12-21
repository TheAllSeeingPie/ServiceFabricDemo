using Microsoft.ServiceFabric.Actors;

namespace SimpleStatelessActor.Interfaces
{
    public interface ISimpleStatelessActorEvents : IActorEvents
    {
        void Ping(string message);
    }
}