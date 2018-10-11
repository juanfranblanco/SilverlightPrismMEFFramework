using System;

using Microsoft.Practices.Prism.ViewModel;

namespace ClaimsModule.Models
{
    public class Claim : NotificationObject
    {
        #region Constants and Fields

        private int claimId;

        private string claimNumber;

        private string claimPrefix;

        private string claimSufix;

        private int policyId;

        #endregion

        #region Properties

        
        public int ClaimId
        {
            get
            {
                return this.claimId;
            }
            set
            {
                this.claimId = value;
                this.RaisePropertyChanged(() => this.ClaimId);
            }
        }

        
        public string ClaimNumber
        {
            get
            {
                return this.claimNumber;
            }
            set
            {
                this.claimNumber = value;
                this.RaisePropertyChanged(() => this.ClaimNumber);
            }
        }

        
        public string ClaimPrefix
        {
            get
            {
                return this.claimPrefix;
            }
            set
            {
                this.claimPrefix = value;
                this.RaisePropertyChanged(() => this.ClaimPrefix);
            }
        }

       
        public string ClaimSufix
        {
            get
            {
                return this.claimSufix;
            }
            set
            {
                this.claimSufix = value;
                this.RaisePropertyChanged(() => this.ClaimSufix);
            }
        }

        
        public Int32 PolicyId
        {
            get
            {
                return this.policyId;
            }
            set
            {
                this.policyId = value;
                this.RaisePropertyChanged(() => this.PolicyId);
            }
        }

      
        #endregion
    }
}