using ClaimsModule.Models;

namespace ClaimsModule.Events.Payloads
{
    public class ClaimSelectedPayload
    {
        #region Properties

        public Claim Claim { get; set; }

        public int PolicyId { get; set; }

        #endregion
    }
}