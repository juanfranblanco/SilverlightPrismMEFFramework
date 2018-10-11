using ClaimsModule.Models;

namespace ClaimsModule.Services
{
    public interface IClaimDataService : IClaimsRepository
    {
        #region Public Methods

        Claim GetClaim(int claimId);

        #endregion
    }
}