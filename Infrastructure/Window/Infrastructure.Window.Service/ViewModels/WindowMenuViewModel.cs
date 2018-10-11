using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

using Infrastructure.Window.Contracts.Events;
using Infrastructure.Window.Contracts.Model;
using Infrastructure.Window.Contracts.ViewModel;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Infrastructure.UI.Window.Service.ViewModels
{
    [Export(typeof(IWindowMenuViewModel<>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WindowMenuViewModel<T> : NotificationObject, IWindowMenuViewModel<T>
        where T: class
    {
        private readonly DelegateCommand<WindowItem<T>> openWindowCommand;
        private readonly DelegateCommand<WindowItem<T>> closeWindowCommand;
        private readonly IEventAggregator eventAggregator;
        [ImportingConstructor]
        public WindowMenuViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<WindowAdded<T>>().Subscribe(AddWindowItem);
            this.eventAggregator.GetEvent<WindowRemoved<T>>().Subscribe(RemoveWindowItem);
            this.Windows = new ObservableCollection<WindowItem<T>>();
            openWindowCommand = new DelegateCommand<WindowItem<T>>(OpenWindow);
            closeWindowCommand = new DelegateCommand<WindowItem<T>>(CloseWindow); 
        }

        private ObservableCollection<WindowItem<T>> windows;
        public ObservableCollection<WindowItem<T>> Windows
        {
            get { return windows; }
            set { windows = value;
                RaisePropertyChanged(() => Windows);
            }
        }

        public void AddWindowItem(WindowItem<T> windowItem)
        {
            Windows.Add(windowItem);
        }

        public void RemoveWindowItem(WindowItem<T> windowItem)
        {
            Windows.Remove(windowItem);
        }

        public ICommand OpenWindowCommand
        {
            get { return openWindowCommand; }
        }

        public ICommand CloseWindowCommand
        {
            get { return closeWindowCommand; }
        }

        public void OpenWindow(WindowItem<T> windowItem)
        {
            eventAggregator.GetEvent<WindowOpen<T>>().Publish(windowItem);
        }

        public void CloseWindow(WindowItem<T> windowItem)
        {
            eventAggregator.GetEvent<WindowClose<T>>().Publish(windowItem);
        }
    }
}