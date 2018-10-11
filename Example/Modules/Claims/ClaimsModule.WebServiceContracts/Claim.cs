using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClaimsModule.WebServiceMock
{
    public class Claim : object, INotifyPropertyChanged
    {
        #region Constants and Fields

        private int ClaimIdField;

        private string ClaimNumberField;

        private string ClaimPrefixField;

        private string ClaimSufixField;

        private int PolicyIdField;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

       
        public int ClaimId
        {
            get
            {
                return this.ClaimIdField;
            }
            set
            {
                if ((this.ClaimIdField.Equals(value) != true))
                {
                    this.ClaimIdField = value;
                    this.RaisePropertyChanged("ClaimId");
                }
            }
        }

        public string ClaimNumber
        {
            get
            {
                return this.ClaimNumberField;
            }
            set
            {
                if ((ReferenceEquals(this.ClaimNumberField, value) != true))
                {
                    this.ClaimNumberField = value;
                    this.RaisePropertyChanged("ClaimNumber");
                }
            }
        }

        public string ClaimPrefix
        {
            get
            {
                return this.ClaimPrefixField;
            }
            set
            {
                if ((ReferenceEquals(this.ClaimPrefixField, value) != true))
                {
                    this.ClaimPrefixField = value;
                    this.RaisePropertyChanged("ClaimPrefix");
                }
            }
        }

        public string ClaimSufix
        {
            get
            {
                return this.ClaimSufixField;
            }
            set
            {
                if ((ReferenceEquals(this.ClaimSufixField, value) != true))
                {
                    this.ClaimSufixField = value;
                    this.RaisePropertyChanged("ClaimSufix");
                }
            }
        }

        
        public int PolicyId
        {
            get
            {
                return this.PolicyIdField;
            }
            set
            {
                if ((this.PolicyIdField.Equals(value) != true))
                {
                    this.PolicyIdField = value;
                    this.RaisePropertyChanged("PolicyId");
                }
            }
        }

       
        #endregion

        #region Methods

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}