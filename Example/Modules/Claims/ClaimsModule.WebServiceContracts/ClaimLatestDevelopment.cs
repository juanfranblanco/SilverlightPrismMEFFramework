using System.ComponentModel;

namespace ClaimsModule.WebServiceMock
{
    public class ClaimLatestDevelopment : object, INotifyPropertyChanged
    {
        #region Constants and Fields

        private Claim ClaimField;

       
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public Claim Claim
        {
            get
            {
                return this.ClaimField;
            }
            set
            {
                if ((ReferenceEquals(this.ClaimField, value) != true))
                {
                    this.ClaimField = value;
                    this.RaisePropertyChanged("Claim");
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