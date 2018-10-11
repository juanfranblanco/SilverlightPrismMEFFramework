namespace Application.Shell.Contracts.Services
{
    public interface IApplicationCommands
    {
        void CloseView(object view);
        void OpenView(string targetName);
    }
}