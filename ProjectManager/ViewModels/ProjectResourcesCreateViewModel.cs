using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.ViewModels
{
    class ProjectResourcesCreateViewModel : ViewModel
    {

        public ProjectResourcesCreateViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        private NavigationService navigationService;
        private int resourceType = -1;
        private Visibility workingResourceVisibility = Visibility.Collapsed;
        private Visibility materialResourceVisibility = Visibility.Collapsed;
        public int ResourceType
        {           

            get 
            {                
                return resourceType; 
            }
            set 
            {
                Set(ref resourceType, ref value);
                if(resourceType == 0)
                {
                    WorkingResourceVisibility = Visibility.Visible;
                    MaterialResourceVisibility = Visibility.Collapsed;
                }
                else if(resourceType == 1)
                {
                    WorkingResourceVisibility = Visibility.Collapsed;
                    MaterialResourceVisibility = Visibility.Visible;
                }
                else
                {
                    WorkingResourceVisibility = Visibility.Collapsed;
                    MaterialResourceVisibility = Visibility.Collapsed;
                }
            }
        }
        public Visibility WorkingResourceVisibility
        {
            get { return workingResourceVisibility; }
            set
            {
                Set(ref workingResourceVisibility, ref value);
            }
        }
        public Visibility MaterialResourceVisibility
        {
            get { return materialResourceVisibility; }
            set
            {
                Set(ref materialResourceVisibility, ref value);
            }
        }
    }   
}
