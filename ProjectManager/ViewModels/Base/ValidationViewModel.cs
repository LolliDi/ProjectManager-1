using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels.Base
{
    abstract class ValidationViewModel : ViewModel, INotifyDataErrorInfo
    {
        public ValidationViewModel()
        {
            errorsDictionary = new Dictionary<string, List<string>>();
        }

        protected readonly Dictionary<string, List<string>> errorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => errorsDictionary.Any();
        public IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            return errorsDictionary.ContainsKey(propertyName) ? errorsDictionary[propertyName] : null;
        }
        protected void AddError(string errorInfo, [CallerMemberName] string propertyName = null)
        {
            if (!errorsDictionary.ContainsKey(propertyName))
            {
                errorsDictionary.Add(propertyName, new List<string>() { errorInfo });
                OnErrorsChanged(propertyName);
            }
            if (!errorsDictionary[propertyName].Contains(errorInfo))
            {
                errorsDictionary[propertyName].Append(errorInfo);
                OnErrorsChanged(propertyName);
            }
        }
        protected void ClearErrors([CallerMemberName] string propertyName = null)
        {
            if (errorsDictionary.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
