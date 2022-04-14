using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectManager.ViewModels
{
    class ProjectCreateViewModel : FormViewModel
    {

        private FormNavigationService NavigationService;
        private Users currentUser;
        private Repository<Users> usersRepository;

        private object selectedUsers;


        private SolidColorBrush standartColor = new SolidColorBrush(Color.FromRgb(250, 250, 250));

        private string title = "";
        private SolidColorBrush titleBorderColor;
        private List<Users> usersList;
        private List<object> SelectedItems { get; }

        public ProjectCreateViewModel(FormNavigationService navigationService, Users currentUser) : base(navigationService)
        {
            this.currentUser = currentUser;
            NavigationService = navigationService;

            usersRepository = new Repository<Users>();

            titleBorderColor = standartColor;
            UsersList = new List<Users>();

            selectedUsers = new object();
        }

        protected override void OnSubmitCommandExecute(object parameter)
        {
            GoNewProject();
            base.OnSubmitCommandExecute(parameter);
        }

        public string Title
        {
            get => title;
            set => Set(ref title, ref value);
        }

        public SolidColorBrush TitleBorderColor
        {
            get => titleBorderColor;
            set => Set(ref titleBorderColor, ref value);
        }       

        public List<Users> UsersList
        {
            get => usersList;
            set
            {
                List<Users> users = value;
                foreach (Users user in usersRepository.Items)
                {
                    if (user.Username != currentUser.Username && user.Roles.Permissions.Where(x => x.Name == "permission_privateprojects").ToList().Any())
                    {
                        users.Add(user);
                    }
                }
                usersList = users;
            }
        }

        private void GoNewProject()
        {
            Projects newProject = new Projects();
            if (title.Length < 1 || title.Length > 50)
                return;
            newProject.Name = title;
            newProject.CreatingDate = DateTime.Now;
            newProject.User = currentUser.Id;
            newProject.Users.Add(usersRepository.Items.Where(x => x.Id == currentUser.Id).FirstOrDefault()); //Приходится делать так, потому что разные контексты
            var selectedUsersList = ((IEnumerable<object>)selectedUsers).Cast<object>().ToList();
            if (selectedUsersList.Count > 0)
            {
                foreach (var item in selectedUsersList)
                {
                    newProject.Users.Add((Users)item);
                }
            }
            ProjectsRepository projectsRepository = new ProjectsRepository();
            projectsRepository.Add(newProject);
        }        
    }
}
