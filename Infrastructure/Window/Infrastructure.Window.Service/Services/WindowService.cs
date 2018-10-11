namespace Infrastructure.UI.Window.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Infrastructure.Window.Contracts.Events;
    using Infrastructure.Window.Contracts.Model;
    using Infrastructure.Window.Contracts.Services;

    using Microsoft.Practices.Prism.Events;

    [Export(typeof(IWindowService<>))]
    public class WindowService<TKey> : IWindowService<TKey>
        where TKey : class
    {
        private readonly IEventAggregator eventAggregator;
        private List<WindowItem<TKey>> windows;

        [ImportingConstructor]
        public WindowService(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator = eventAggregator;

            windows = new List<WindowItem<TKey>>();
        }

        public Func<TKey, TKey, bool> CompareKeys { get; set; }

        public bool ContainsWindow(TKey key)
        {
            return this.GetWindow(key)!= null;
        }

        public WindowItem<TKey> GetWindow(TKey key)
        {
            return windows.FirstOrDefault(x => this.CheckKeysAreTheSame(x.Key, key));
        }

        public bool CheckKeysAreTheSame(TKey firstKey, TKey secondKey)
        {
            if (CompareKeys == null)
            {
                return firstKey == secondKey;
            }
            else
            {
                return CompareKeys(firstKey, secondKey);    
            }
        }

        public void RemoveWindow(TKey key)
        {
            if (!ContainsWindow(key)) return;

            var windowItemViewModel = GetWindow(key);
            windows.Remove(windowItemViewModel);
            eventAggregator.GetEvent<WindowRemoved<TKey>>().Publish(windowItemViewModel);
        }


        public WindowItem<TKey> CreateWindow(TKey key, Object currentView)
        {
            var windowItemViewModel = new WindowItem<TKey> { CurrentView = currentView, Key = key };
            windows.Add(windowItemViewModel);
            this.eventAggregator.GetEvent<WindowAdded<TKey>>().Publish(windowItemViewModel);
            return windowItemViewModel;
        }
    }
}