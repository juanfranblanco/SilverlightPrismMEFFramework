using Microsoft.Practices.Prism.Events;

using Policy.Contracts.Models;

namespace Policy.Search.Events
{
    public class PolicySearchFoundResultEvent : CompositePresentationEvent<PolicySearchResult>
    {
    }
}