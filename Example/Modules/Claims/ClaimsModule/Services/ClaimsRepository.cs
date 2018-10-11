using System;
using System.ComponentModel.Composition;
using System.Threading;

using ClaimsModule.DataMapping;
using ClaimsModule.Models;
using ClaimsModule.WebServiceMock;

using Infrastructure.Core.Services;

namespace ClaimsModule.Services
{
    [Export(typeof(IClaimsRepository))]
    public class ClaimsRepository : IClaimsRepository
    {
        #region Constants and Fields

        private readonly IClaimModelDataContractMapper claimModelDataContractMapper;

        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current ??
                                                                         new SynchronizationContext();

        #endregion

        #region Constructors and Destructors

        
        [ImportingConstructor]
        public ClaimsRepository(IClaimModelDataContractMapper claimModelDataContractMapper, IClaimsServiceWS claimsServiceWS)
        {
            this.claimModelDataContractMapper = claimModelDataContractMapper;
            this.ClaimsServiceWS = claimsServiceWS;
        }

        #endregion

        #region Properties

        public IClaimsServiceWS ClaimsServiceWS { get; set; }

        #endregion

        #region Implemented Interfaces

        #region IClaimsRepository

        public void GetClaimsForCostingAsync(int costingId, Action<IOperationResult<ClaimsCollection>> callback)
        {
            this.ClaimsServiceWS.BeginFindClaimsForCosting(
                costingId,
                (ar) =>
                    {
                        var operationResult = new OperationResult<ClaimsCollection>();
                        try
                        {
                            ClaimsCollection claims =
                                this.claimModelDataContractMapper.MapClaimLatestDevelopmentsToClaimsCollection(
                                    this.ClaimsServiceWS.EndFindClaimsForCosting(ar));
                            operationResult.Result = claims;
                        }
                        catch (Exception ex)
                        {
                            operationResult.Error = ex;
                        }

                        this.synchronizationContext.Post((state) => callback(operationResult), null);
                    },
                null);
        }

        #endregion

        #endregion
    }
}