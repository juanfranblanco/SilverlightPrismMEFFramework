namespace Infrastructure.Window.Contracts.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Infrastructure.Window.Contracts.Model;

    public interface IWindowMenuViewModel<T>
        where T : class
    {
        ObservableCollection<WindowItem<T>> Windows { get; set; }

        ICommand OpenWindowCommand { get; }

        ICommand CloseWindowCommand { get; }
    }
}