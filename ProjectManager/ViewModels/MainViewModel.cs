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
            Repository<Users> repository = new Repository<Users>(new ProjectManagerContext());
            Users user = repository.Get(1);

            NavigationService = new NavigationService(new UserAccountViewModel(user));
            ToUserAccountViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UserAccountViewModel(user));
        }
        public NavigationService NavigationService { get; }
        public ICommand ToUserAccountViewModel { get; set; }
    }
}
