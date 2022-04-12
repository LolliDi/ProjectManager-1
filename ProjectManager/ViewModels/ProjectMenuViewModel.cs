using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        public ProjectMenuViewModel(NavigationService navigationService, Projects project, Users user)
        {
            CurrentProject = project;
            CurrentUser = user;

            MainNavigationService = navigationService;

            ProjectMenuNavigationService = new NavigationService();
            ProjectMenuNavigationService.CurrentViewModel = new ProjectTasksViewModel(CurrentProject);

            ToProjectTasksPageCommand = new LambdaCommand(OnToProjectTasksPageCommandExecute, CanToProjectTasksPageCommandExecuted);
            ToProjectResourcePageCommand = new LambdaCommand(OnToProjectResourcePageCommandExecute, CanToProjectResourcePageCommandExecuted);
            ToProjectReportPageCommand = new LambdaCommand(OnToProjectReportPageCommandExecute, CanToProjectReportPageCommandExecuted);
            ToProjectPassportPageCommand = new LambdaCommand(OnToProjectPassportPageCommandExecute, CanToProjectPassportPageCommandExecuted);
            ToAdminPageCommand = new LambdaCommand(OnToAdminPageCommandExecute);
            ToAuthorizationPageCommand = new LambdaCommand(OnToAuthorizationPageCommandExecute);
        }

        #region Fields


        #endregion

        #region Properties

        public Projects CurrentProject { get; }
        public Users CurrentUser { get; }

        public NavigationService ProjectMenuNavigationService { get; set; }
        public NavigationService MainNavigationService { get; set; }

        #endregion

        #region Commands

        public ICommand ToProjectTasksPageCommand { get; set; }
        public ICommand ToProjectResourcePageCommand { get; set; }
        public ICommand ToProjectReportPageCommand { get; set; }
        public ICommand ToProjectPassportPageCommand { get; set; }
        public ICommand ToAdminPageCommand { get; set; }
        public ICommand ToAuthorizationPageCommand { get; set; }

        #endregion

        #region Private Methods

        private void OnToProjectTasksPageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectTasksViewModel(CurrentProject);
        }
        private void OnToProjectResourcePageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectResourcesViewModel(ProjectMenuNavigationService, CurrentProject);
        }
        private void OnToProjectReportPageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectReportViewModel(ProjectMenuNavigationService, CurrentProject);
        }
        private void OnToProjectPassportPageCommandExecute(object parameter)
        {
            ProjectMenuNavigationService.CurrentViewModel = new ProjectPassportViewModel(ProjectMenuNavigationService, CurrentProject);
        }
        private void OnToAdminPageCommandExecute(object parameter)
        {
            MessageBox.Show("Это еще не добавили!", "Упс...", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void OnToAuthorizationPageCommandExecute(object parameter)
        {
            var dialogResult = MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                MainNavigationService.CurrentViewModel = new AuthorizationViewModel(MainNavigationService);
            }
        }
        private bool CanToProjectTasksPageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectTasksViewModel);
        }
        private bool CanToProjectResourcePageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectResourcesViewModel);
        }
        private bool CanToProjectReportPageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectReportViewModel);
        }
        private bool CanToProjectPassportPageCommandExecuted(object parameter)
        {
            return ProjectMenuNavigationService.CurrentViewModel.GetType() != typeof(ProjectPassportViewModel);
        }

        #endregion
    }
}
