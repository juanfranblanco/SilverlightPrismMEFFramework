using System;

using ClaimsModule.Models;

using Infrastructure.Core.Services;

namespace ClaimsModule.Services
{
    public interface IClaimsRepository
    {
        #region Public Methods

        void GetClaimsForCostingAsync(int costingId, Action<IOperationResult<ClaimsCollection>> callback);

        #endregion
    }
}