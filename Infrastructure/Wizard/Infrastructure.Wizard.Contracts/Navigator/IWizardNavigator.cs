namespace Infrastructure.Wizard.Contracts.Navigator
{
    public interface IWizardNavigator<TWizardContext>
    {
        void OpenView(string targetName, TWizardContext wizardContext);
    }
}