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
                if(value == false)
                {
                    CurrentViewModel = null;
                }
                isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }
    }
}
