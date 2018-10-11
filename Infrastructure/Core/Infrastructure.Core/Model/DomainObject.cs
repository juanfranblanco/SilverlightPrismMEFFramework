using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.Practices.Prism.ViewModel;

namespace Infrastructure.Core.Model
{
    /// <summary>
    /// Base domain object class.
    /// </summary>
    /// <remarks>
    /// Provides support for implementing <see cref="INotifyPropertyChanged"/> and 
    /// implements <see cref="INotifyDataErrorInfo"/> using <see cref="ValidationAttribute"/> instances
    /// on the validated properties.
    /// </remarks>
    public abstract class DomainObject : NotificationObject, INotifyDataErrorInfo
    {
        private ErrorsContainer<ValidationResult> errorsContainer;

        /// <summary>
        /// Gets the container for errors in the properties of the domain object.
        /// </summary>
        protected ErrorsContainer<ValidationResult> ErrorsContainer
        {
            get
            {
                if (errorsContainer == null)
                {
                    errorsContainer =
                        new ErrorsContainer<ValidationResult>(pn => RaiseErrorsChanged(pn));
                }

                return errorsContainer;
            }
        }

        #region INotifyDataErrorInfo Members

        /// <summary>
        /// Event raised when the validation status changes.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the error status.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        /// <summary>
        /// Returns the errors for <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property for which the errors are requested.</param>
        /// <returns>An enumerable with the errors.</returns>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        #endregion

        public void ValidateObject()
        {
            ErrorsContainer.ClearErrors("");
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults);
            ErrorsContainer.SetErrors("", validationResults);
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            ValidateObject();
            base.RaisePropertyChanged(propertyName);
        }


        /// <summary>
        /// Validates <paramref name="value"/> as the value for the property named <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value for the property.</param>
        protected void ValidateProperty(string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            ValidateProperty(new ValidationContext(this, null, null) {MemberName = propertyName}, value);
        }

        /// <summary>
        /// Validates <paramref name="value"/> as the value for the property specified by 
        /// <paramref name="validationContext"/> using data annotations validation attributes.
        /// </summary>
        /// <param name="validationContext">The context for the validation.</param>
        /// <param name="value">The value for the property.</param>
        protected virtual void ValidateProperty(ValidationContext validationContext, object value)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);


            ErrorsContainer.SetErrors(validationContext.MemberName, validationResults);
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which changed its error status.</param>
        protected void RaiseErrorsChanged(string propertyName)
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="e">The argument for the event.</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            EventHandler<DataErrorsChangedEventArgs> handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}