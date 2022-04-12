using ProjectManager.Commands;
using ProjectManager.Services;
using ProjectManager.ViewModels;
using ProjectManager.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    abstract class FormViewModel : ValidationViewModel, IFormViewModel
    {
        public FormViewModel(FormNavigationService formNavigationService)
        {
            FormNavigationService = formNavigationService;

            SubmitCommand = new LambdaCommand(OnSubmitCommandExecute, CanSubmitCommandExecuted);
            CancelCommand = new LambdaCommand(OnCancelCommandExecute, CanCancelCommandExecuted);
        }

        private string header;

        public FormNavigationService FormNavigationService { get; set; }
        public string Header
        {
            get => header;
            set => Set(ref header, ref value);
        }
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        protected virtual void OnSubmitCommandExecute(object parameter)
        {
            FormNavigationService.IsOpen = false;
        }
        protected virtual bool CanSubmitCommandExecuted(object parameter)
        {
            return true;
        }
        protected virtual void OnCancelCommandExecute(object parameter)
        {
            FormNavigationService.IsOpen = false;
        }
        protected virtual bool CanCancelCommandExecuted(object parameter)
        {
            return true;
        }
    }
}
