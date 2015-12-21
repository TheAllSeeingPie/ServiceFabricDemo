using System;
using System.Threading.Tasks;
using GrumpyActor.Interfaces;
using Microsoft.ServiceFabric.Actors;
using MultipleActors.Interfaces;

namespace GrumpyActor
{
    /// <remarks>
    /// Each ActorID maps to an instance of this class.
    /// The IGrumpyActor interface (in a separate DLL that client code can
    /// reference) defines the operations exposed by GrumpyActor objects.
    /// </remarks>
    [ActorService(Name = "GrumpyActorService")]
    internal class GrumpyActor : StatelessActor, IGrumpyActor
    {
        public async Task<string> SpeakAsync()
        {
            var @event = GetEvent<ITalkingActorEvent>();
            @event.EnteredMethod("SpeakAsync", "GrumpyActor", Id);
            await Task.Delay(TimeSpan.FromSeconds(5));
            return $"What do you want!!?! (ActorId: {Id})";
        }
    }
}
