using System;
using System.Collections.ObjectModel;

namespace ClaimsModule.WebServiceMock
{
    public interface IClaimsServiceWS
    {
        #region Public Methods

        IAsyncResult BeginFindClaimsForCosting(int costingId, AsyncCallback callback, object asyncState);

        ObservableCollection<ClaimLatestDevelopment> EndFindClaimsForCosting(IAsyncResult result);

        #endregion
    }
}