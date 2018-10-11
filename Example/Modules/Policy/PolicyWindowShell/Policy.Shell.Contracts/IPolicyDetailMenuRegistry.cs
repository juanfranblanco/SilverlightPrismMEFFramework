using System;

namespace Policy.Shell.Contracts
{
    public interface IPolicyDetailMenuRegistry
    {
        void RegisterMenuItemViewOpener(string menuItemName, string description, Type viewType, string targetName, int menuOrderHint);
        void RegisterMenuItemAction(string menuItemName, string description, int menuOrderHint, Action<int> policyIdAction);
    }
}