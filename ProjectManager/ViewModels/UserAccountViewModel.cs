using ProjectManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using ProjectManager.Models;
using ProjectManager.Services;
using ProjectManager.Commands;

namespace ProjectManager.ViewModels
{
    class UserAccountViewModel : ViewModel
    {
        private Users user;
        NavigationService NavigationService;
        public ICommand Back { get; set; }
        public UserAccountViewModel(Users user, NavigationService NavigationService)
        {
            this.user = user;
            this.NavigationService = NavigationService;
            Back = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectListViewModel(NavigationService,user));
        }
        public Users User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
            }
        }
    }
}
