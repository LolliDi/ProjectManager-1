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
            usersRepository = new Repository<Users>(new ProjectManagerContext());
            projectsRepository = new Repository<Projects>(new ProjectManagerContext());
            NavigationService = new NavigationService() { CurrentViewModel = new UserAccountViewModel(null) };

            Users user = usersRepository.Get(3);

            ToUserAccountViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UserAccountViewModel(user));
            ToProjectMenuViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectMenuViewModel(user, projectsRepository));
            ToViewModelAuto = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new AuthorizationViewModel(NavigationService));
            ToProjectTasksViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectTasksViewModel(NavigationService, null));
        }

        private readonly IRepository<Users> usersRepository;
        private readonly IRepository<Projects> projectsRepository;

        public NavigationService NavigationService { get; set; }
        public ICommand ToUserAccountViewModel { get; set; }
        public ICommand ToProjectMenuViewModel { get; set; }
        public ICommand ToViewModelAuto { get; set; }
        public ICommand ToProjectTasksViewModel { get; set; }

    }
}
