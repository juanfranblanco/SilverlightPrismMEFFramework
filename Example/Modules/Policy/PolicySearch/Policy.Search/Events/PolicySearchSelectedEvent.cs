using Microsoft.Practices.Prism.Events;

namespace Policy.Search.Events
{
    /// <summary>
    /// Event raised when a policy gets selected
    /// </summary>
    public class PolicySearchSelectedEvent : CompositePresentationEvent<Policy.Contracts.Models.Policy>
    {
    }
}