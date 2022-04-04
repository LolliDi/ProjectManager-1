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
            NavigationService = new NavigationService() { CurrentViewModel = new UserAccountViewModel(null, NavigationService) };

            Users user = usersRepository.Get(2);

            ToUserAccountViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UserAccountViewModel(user, NavigationService));
            ToProjectMenuViewModel = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectMenuViewModel(NavigationService, user));
            ToViewModelAuto = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new AuthorizationViewModel(NavigationService));
            ToAddUserPage = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new AddUserPageViewModel());
            ToUsersViewPage = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new UsersViewPageViewModel());
        }

        private readonly IRepository<Users> usersRepository;

        public NavigationService NavigationService { get; set; }
        public ICommand ToUserAccountViewModel { get; set; }
        public ICommand ToProjectMenuViewModel { get; set; }
        public ICommand ToViewModelAuto { get; set; }
        public ICommand ToAddUserPage { get; set; }
        public ICommand ToUsersViewPage { get; set; }
    }
}
