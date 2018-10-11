using ClaimsModule.Events.Payloads;

using Microsoft.Practices.Prism.Events;

namespace ClaimsModule.Events
{
    public class ClaimSelectedEvent : CompositePresentationEvent<ClaimSelectedPayload>
    {
    }
}