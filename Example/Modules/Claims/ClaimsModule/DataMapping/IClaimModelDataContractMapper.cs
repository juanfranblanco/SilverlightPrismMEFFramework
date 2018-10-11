using System.Collections.Generic;

using ClaimsModule.Models;
using ClaimsModule.WebServiceMock;

using Claim = ClaimsModule.Models.Claim;

namespace ClaimsModule.DataMapping
{
    public interface IClaimModelDataContractMapper
    {
        #region Public Methods

        Claim MapClaimLatestDevelopmentToClaim(ClaimLatestDevelopment claimLatestDevelopment);

        ClaimsCollection MapClaimLatestDevelopmentsToClaimsCollection(
            IEnumerable<ClaimLatestDevelopment> claimLatestDevelopments);

        #endregion
    }
}