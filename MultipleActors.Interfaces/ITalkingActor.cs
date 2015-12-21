using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace MultipleActors.Interfaces
{
    public interface ITalkingActor : IActor, IActorEventPublisher<ITalkingActorEvent>
    {
        Task<string> SpeakAsync();
    }
}
