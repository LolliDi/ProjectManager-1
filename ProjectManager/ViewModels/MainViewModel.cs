using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            NavigationService = new NavigationService();
            NavigationService.CurrentViewModel = new AuthorizationViewModel(NavigationService);
        }

        public NavigationService NavigationService { get; set; }
    }
}
