namespace Policy.Shell.Contracts
{
    using Model = Policy.Contracts.Models;

    public interface IPolicyWindowService
    {
        void OpenPolicyWindow(Model.Policy policy);
    }
}