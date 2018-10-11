using System;

using Microsoft.Practices.Prism.ViewModel;

namespace Infrastructure.Window.Contracts.Model
{
    public class WindowItem<TKey> : NotificationObject where TKey : class
    {
        private TKey key;
        public TKey Key
        {
            get { return key; }
            set
            {
                key = value;
                RaisePropertyChanged(() => Key);

            }
        }

        private object currentView;
        public Object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                RaisePropertyChanged(() => CurrentView);
            }
        }
    }
}