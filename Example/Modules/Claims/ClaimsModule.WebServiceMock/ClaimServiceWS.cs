using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading;

using Infrastructure.Core.Async;

namespace ClaimsModule.WebServiceMock
{
    [Export(typeof(IClaimsServiceWS))]
    public class ClaimServiceWS : IClaimsServiceWS
    {
        // This is to support demonstration of a failed submit.

        #region Properties

        public static bool FailOnSubmit { get; set; }

        #endregion

        #region Public Methods

        public IEnumerable<ClaimLatestDevelopment> CreateClaims(int costingId)
        {
            if (costingId == 1)
            {
                return new ObservableCollection<ClaimLatestDevelopment>
                    {
                        new ClaimLatestDevelopment
                            {
                                Claim =
                                    new Claim
                                        {
                                            ClaimId = 1,
                                            ClaimNumber = "1234",
                                            ClaimPrefix = "US",
                                            ClaimSufix = "The end",
                                        }
                            },

                        new ClaimLatestDevelopment
                            {
                                Claim =
                                    new Claim
                                        {
                                            ClaimId = 2,
                                            ClaimNumber = "2345",
                                            ClaimPrefix = "UK",
                                            ClaimSufix = "The end",
                                        }
                            },

                             new ClaimLatestDevelopment
                            {
                                Claim =
                                    new Claim
                                        {
                                            ClaimId = 3,
                                            ClaimNumber = "3456",
                                            ClaimPrefix = "SP",
                                            ClaimSufix = "El final",
                                        }
                            }
                    };
            }
            return new ObservableCollection<ClaimLatestDevelopment>();
        }

        #endregion

        #region Implemented Interfaces

        #region IClaimsServiceWS

        public IAsyncResult BeginFindClaimsForCosting(int costingId, AsyncCallback callback, object asyncState)
        {
            var asyncResult = new AsyncResult<IEnumerable<ClaimLatestDevelopment>>(callback, asyncState);
            ThreadPool.QueueUserWorkItem(o => { asyncResult.SetComplete(this.CreateClaims(costingId), false); });

            return asyncResult;
        }

        public ObservableCollection<ClaimLatestDevelopment> EndFindClaimsForCosting(IAsyncResult result)
        {
            AsyncResult<IEnumerable<ClaimLatestDevelopment>> localAsyncResult =
                AsyncResult<IEnumerable<ClaimLatestDevelopment>>.End(result);

            return (ObservableCollection<ClaimLatestDevelopment>)localAsyncResult.Result;
        }

        #endregion

        #endregion
    }
}