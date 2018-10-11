using System;

namespace Application.Shell.Contracts.Services
{
    public interface IApplicationMenuRegistry
    {
        void RegisterMenuItemViewOpener(string menuItemName, string description, Type viewType, string targetName, int orderHint);
        void RegisterMenuItemAction(string menuItemName, string description, int menuOrderHint, Action<object> action);
    }
}