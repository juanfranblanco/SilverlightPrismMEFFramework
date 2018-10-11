namespace Policy.Shell.Menu.Commands
{
    public interface IPolicyDetailMenuCommands
    {
        void CloseView(object view);
        void OpenView(string targetName, int policyId);
    }
}