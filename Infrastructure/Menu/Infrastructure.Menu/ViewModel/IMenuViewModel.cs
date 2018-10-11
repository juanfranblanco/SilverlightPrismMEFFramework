namespace Infrastructure.Menu
{
    using System.Collections.ObjectModel;

    public interface IMenuViewModel<TMenuContext>
    {
        ObservableCollection<IMenuItem<TMenuContext>> MenuItems { get; }
        void AddMenuItem(IMenuItem<TMenuContext> menuItem);
    }
}