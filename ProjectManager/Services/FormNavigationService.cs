using ProjectManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    class FormNavigationService : NavigationService
    {
        public event Action Creating;
        public event Action Closing;

        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if(Equals(isOpen, value))
                {
                    return;
                }
                if(value == true)
                {
                    OnCreatinForm();
                }
                if(value == false)
                {
                    OnClosingForm();
                    CurrentViewModel = null;
                }
                isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        public Action OnClosingFormCallback { get; set; }
        public Action OnCreatinFormCallback { get; set; }

        public virtual void OnClosingForm()
        {
            OnClosingFormCallback?.Invoke();
            Closing?.Invoke();
        }
        public virtual void OnCreatinForm()
        {
            OnCreatinFormCallback?.Invoke();
            Creating?.Invoke();
        }

    }
}
