using ProjectManager.Commands;
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
            ToViewModel1Commmand = new LambdaCommand(parameter => CurrentViewModel = new ViewModel1());
            ToViewModel2Commmand = new LambdaCommand(parameter => CurrentViewModel = new ViewModel2());
        }
        private ViewModel currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set => Set(ref currentViewModel, ref value);
        }
        public ICommand ToViewModel1Commmand { get; set; }
        public ICommand ToViewModel2Commmand { get; set; }
    }
}
