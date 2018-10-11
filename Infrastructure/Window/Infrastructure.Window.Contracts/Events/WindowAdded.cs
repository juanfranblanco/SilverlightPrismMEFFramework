using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Window.Contracts.Events
{
    using Infrastructure.Window.Contracts.Model;

    public class WindowAdded<TKey> : CompositePresentationEvent<WindowItem<TKey>> where TKey : class
    {
    }
}