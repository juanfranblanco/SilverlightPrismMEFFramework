using System.Runtime.Serialization;

using ClaimsModule.Models;

namespace ClaimsModule.State
{
    [DataContract]
    public class ClaimModuleState
    {
        #region Properties

        [DataMember]
        public ClaimsCollection Claims { get; set; }

        #endregion
    }
}