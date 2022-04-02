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
            Users user = repository.Get(1);
            ToViewModel1Commmand = new LambdaCommand(parameter => CurrentViewModel = new ViewModel1());
            ToViewModel2Commmand = new LambdaCommand(parameter => CurrentViewModel = new ViewModel2());
            ToUserAccountViewModel = new LambdaCommand(parameter => CurrentViewModel = new UserAccountViewModel(user));
            ToViewModelAuto = new LambdaCommand(parameter => CurrentViewModel = new AuthorizationViewModel());
        }
        private ViewModel currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set => Set(ref currentViewModel, ref value);
        }
        public ICommand ToViewModel1Commmand { get; set; }
        public ICommand ToViewModel2Commmand { get; set; }
        public ICommand ToUserAccountViewModel { get; set; }
        public ICommand ToViewModelAuto { get; set; }
    }
}
