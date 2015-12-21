using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace SimpleStatelessActor.Interfaces
{
    public interface ISimpleStatelessActor : IActor, IActorEventPublisher<ISimpleStatelessActorEvent>
    {
        Task<string> SayHelloAsync(string name);
        Task DoWorkAsync(int period);
    }
}