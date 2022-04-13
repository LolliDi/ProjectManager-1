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
            usersRepository = new Repository<Users>();
            projectsRepository = new Repository<Projects>();
            NavigationService = new NavigationService();
            NavigationService.CurrentViewModel = new AuthorizationViewModel(NavigationService);

            Users user = usersRepository.Get(1);
            Projects project = projectsRepository.Get(1);

            ToUserAccountViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UserAccountViewModel(user, NavigationService));
            ToProjectListViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectListViewModel(NavigationService, user));
            ToViewModelAuto = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new AuthorizationViewModel(NavigationService));
            ToAddUserPage = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new AddUserPageViewModel());
            ToUsersViewPage = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UsersViewPageViewModel());
            ToProjectMenuViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectMenuViewModel(NavigationService, project, user));
        }

        private readonly IRepository<Users> usersRepository;
        private readonly IRepository<Projects> projectsRepository;

        public NavigationService NavigationService { get; set; }
        public ICommand ToUserAccountViewModel { get; set; }
        public ICommand ToProjectListViewModel { get; set; }
        public ICommand ToViewModelAuto { get; set; }
        public ICommand ToAddUserPage { get; set; }
        public ICommand ToUsersViewPage { get; set; }
        public ICommand ToProjectMenuViewModel { get; set; }
    }
}
