using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
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
            var r = new ProjectManagerContext();
            var f = r.Users.First();
            Repository<Users> repository = new Repository<Users>(new ProjectManagerContext());
            Repository<Projects> repositoryPr = new Repository<Projects>(new ProjectManagerContext());
            Users user = repository.Get(1);
            List<Projects> projects = new List<Projects>() { repositoryPr.Get(1) };
            ToUserAccountViewModel = new LambdaCommand(parameter => CurrentViewModel = new UserAccountViewModel(user));
            ToProjectMenuViewModel = new LambdaCommand(parameter => CurrentViewModel = new ProjectMenuViewModel(projects));
            ToViewModelAuto = new LambdaCommand(parameter => CurrentViewModel = new AuthorizationViewModel());
        }
        private ViewModel currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set => Set(ref currentViewModel, ref value);
        }
        public ICommand ToUserAccountViewModel { get; set; }
        public ICommand ToProjectMenuViewModel { get; set; }
        public ICommand ToViewModelAuto { get; set; }
    }
}
