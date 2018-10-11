using System;

namespace Infrastructure.Window.Contracts.Services
{
    using Infrastructure.Window.Contracts.Model;

    public interface IWindowService<TKey>
      where TKey : class
    {
        bool ContainsWindow(TKey key);

        WindowItem<TKey> GetWindow(TKey key);

        void RemoveWindow(TKey key);

        WindowItem<TKey> CreateWindow(TKey key, Object currentView);

        Func<TKey, TKey, bool> CompareKeys { get; set; }
    } 
}
