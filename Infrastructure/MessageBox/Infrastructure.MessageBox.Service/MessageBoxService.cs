namespace Infrastructure.MessageBox.Service
{
    using System;
    using System.ComponentModel.Composition;

    using global::MessageBox.Contracts;

    using SilverlightMessageBoxLibrary;

    [Export(typeof(IMessageBoxService))]
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowConfirm(string message, Action yesAction, Action noAction)
        {
            //downloaded from http://silverlightmsgbox.codeplex.com/
            var confirmMessage = new CustomMessage(message, CustomMessage.MessageType.Confirm);
            confirmMessage.CancelButton.Click += (obj, args) => noAction();
            confirmMessage.OKButton.Click += (obj, args) => yesAction();
            confirmMessage.Show();
        }

        public void ShowMessage(string message)
        {
            var messsageBox = new CustomMessage(message);
            messsageBox.Show();
        }

    
    }
}
