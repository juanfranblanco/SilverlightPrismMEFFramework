namespace MessageBox.Contracts
{
    using System;

    public interface IMessageBoxService
    {
        void ShowConfirm(string message, Action yesAction, Action noAction);

        void ShowMessage(string message);
    }
}