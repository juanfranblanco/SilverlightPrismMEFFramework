using System.Collections.Generic;
using System.ComponentModel.Composition;

using AutoMapper;

using ClaimsModule.Models;
using ClaimsModule.WebServiceMock;

using Claim = ClaimsModule.Models.Claim;


namespace ClaimsModule.DataMapping
{
    [Export(typeof(IClaimModelDataContractMapper))]
    public class ClaimModelDataContractMapper : IClaimModelDataContractMapper
    {
        #region Implemented Interfaces

        #region IClaimModelDataContractMapper

        public Claim MapClaimLatestDevelopmentToClaim(ClaimLatestDevelopment claimLatestDevelopment)
        {
            Mapper.CreateMap<WebServiceMock.Claim, Claim>();
           
            Claim claim = Mapper.Map<WebServiceMock.Claim, Claim>(claimLatestDevelopment.Claim);
            return claim;
        }

        public ClaimsCollection MapClaimLatestDevelopmentsToClaimsCollection(
            IEnumerable<ClaimLatestDevelopment> claimLatestDevelopments)
        {
            var claimsCollection = new ClaimsCollection();
            foreach (ClaimLatestDevelopment claimLatestDevelopment in claimLatestDevelopments)
            {
                claimsCollection.Add(this.MapClaimLatestDevelopmentToClaim(claimLatestDevelopment));
            }
            return claimsCollection;
        }

        #endregion

        #endregion
    }
}