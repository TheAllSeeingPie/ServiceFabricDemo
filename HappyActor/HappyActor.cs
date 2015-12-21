using System;
using System.Threading.Tasks;
using HappyActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using MultipleActors.Interfaces;

namespace HappyActor
{
    /// <remarks>
    /// Each ActorID maps to an instance of this class.
    /// The IHappyActor interface (in a separate DLL that client code can
    /// reference) defines the operations exposed by HappyActor objects.
    /// </remarks>
    [ActorService(Name = "HappyActorService")]
    internal class HappyActor : StatelessActor, IHappyActor
    {
        public async Task<string> SpeakAsync()
        {
            var @event = GetEvent<ITalkingActorEvent>();
            @event.EnteredMethod("SpeakAsync", "HappyActor", Id);
            var grumpyProxy = ActorProxy.Create<ITalkingActor>(new ActorId("MultipleActorDemo"), new Uri("fabric:/MultipleActors/GrumpyActorService"));
            await grumpyProxy.SpeakAsync();
            return $"Hi! How are you? (ActorId: {Id})";
        }
    }
}
