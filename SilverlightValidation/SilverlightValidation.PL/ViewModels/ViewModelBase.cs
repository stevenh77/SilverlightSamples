using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SilverlightValidation.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region INotifyPropertyChanged method plus event

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyDataErrorInfo methods and helpers

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public void SetError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors.Add(propertyName, new List<string> { errorMessage });

            RaiseErrorsChanged(propertyName);
        }

        protected void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

        protected void ClearAllErrors()
        {
            var errors = _errors.Select(error => error.Key).ToList();

            foreach (var propertyName in errors)
                ClearError(propertyName);
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null) return null;
            return _errors.ContainsKey(propertyName)
                    ? _errors[propertyName]
                    : null;
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        #endregion
    }
}
