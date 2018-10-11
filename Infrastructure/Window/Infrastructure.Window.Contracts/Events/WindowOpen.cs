using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Window.Contracts.Events
{
    using Infrastructure.Window.Contracts.Model;

    public class WindowOpen<TKey> : CompositePresentationEvent<WindowItem<TKey>> where TKey : class
    {

    }
}