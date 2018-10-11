using Policy.Shell.Contracts;

namespace Policy.Shell.Services.ContextServices
{
    public class PolicyDetailContext:IPolicyDetailContext
    {
        public int CurrentPolicyId { get; private set; }

        public PolicyDetailContext(int policyId)
        {
            this.CurrentPolicyId = policyId;
        }
        
    }
}
