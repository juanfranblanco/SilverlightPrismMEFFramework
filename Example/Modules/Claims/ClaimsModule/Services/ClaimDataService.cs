using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;

using ClaimsModule.Models;
using ClaimsModule.State;

using Infrastructure.Core.Services;

using Policy.Shell.Contracts;

using UI.Infrastructure.State.Contracts.Services;

namespace ClaimsModule.Services
{
    [Export(typeof(IClaimDataService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ClaimDataService : IClaimDataService
    {
        #region Constants and Fields

        private readonly IClaimsRepository claimsRepository;

        private readonly IPolicyDetailContext policyDetailContext;

        private readonly IStateService stateService;

        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current ??
                                                                         new SynchronizationContext();

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ClaimDataService(
            IClaimsRepository claimsRepository, IPolicyDetailContext policyDetailContext, IStateService stateService)
        {
            if (claimsRepository == null)
            {
                throw new ArgumentNullException("claimsRepository");
            }
            if (policyDetailContext == null)
            {
                throw new ArgumentNullException("policyDetailContext");
            }
            if (stateService == null)
            {
                throw new ArgumentNullException("stateService");
            }

            this.claimsRepository = claimsRepository;
            this.policyDetailContext = policyDetailContext;
            this.stateService = stateService;

            this.InitialiseClaimModuleState();
        }

        #endregion

        #region Properties

        public ClaimModuleState ClaimModuleState { get; set; }

        #endregion

        //We retrieve a claim from our current store.. 

        #region Implemented Interfaces

        #region IClaimDataService

        public Claim GetClaim(int claimId)
        {
            return this.ClaimModuleState.Claims.Where(claim => claim.ClaimId == claimId).FirstOrDefault();
        }

        #endregion

        #region IClaimsRepository

        /// <summary>
        /// This is the main entry point in our logic, here we retrieve all the claims and if there is no error we store in memory
        /// </summary>
        /// <param name="costingId"></param>
        /// <param name="callback"></param>
        public void GetClaimsForCostingAsync(int costingId, Action<IOperationResult<ClaimsCollection>> callback)
        {
            //Check if we have it already
            if (this.ClaimModuleState.Claims == null)
            {
                //go and get it from the web service
                this.claimsRepository.GetClaimsForCostingAsync(
                    costingId,
                    (operationResult) =>
                        {
                            if (operationResult.Error == null)
                            {
                                this.ClaimModuleState.Claims = operationResult.Result;
                            }

                            this.synchronizationContext.Post((state) => callback(operationResult), null);
                        });
            }
            else
            {
                this.synchronizationContext.Post(
                    (state) => callback(new OperationResult<ClaimsCollection> { Result = this.ClaimModuleState.Claims }),
                    null);
            }
        }

        #endregion

        #endregion

        #region Methods

        private void InitialiseClaimModuleState()
        {
            this.ClaimModuleState =
                this.stateService.RestoreValues<ClaimModuleState>(this.policyDetailContext.CurrentPolicyId.ToString());

            if (this.ClaimModuleState == null)
            {
                this.ClaimModuleState = new ClaimModuleState();
                this.stateService.RegisterSaveableObject(
                    this.ClaimModuleState, this.policyDetailContext.CurrentPolicyId.ToString());
            }
        }

        #endregion
    }
}