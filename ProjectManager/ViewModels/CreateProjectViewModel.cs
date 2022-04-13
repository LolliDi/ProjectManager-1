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
using System.Windows.Media;

namespace ProjectManager.ViewModels
{
    class CreateProjectViewModel : ViewModel
    {
        private NavigationService NavigationService;
        private Users currentUser;
        private Repository<Users> usersRepository;


        private SolidColorBrush standartColor = new SolidColorBrush(Color.FromRgb(250, 250, 250));
        private SolidColorBrush redColor = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        private string title = "";
        private SolidColorBrush titleBorderColor;
        private List<Users> usersList;

        public ICommand ToNewProject { get; set; }
        public ICommand ToBack { get; set; }
        
        public CreateProjectViewModel(NavigationService navigationService, Users currentUser)
        {
            this.currentUser = currentUser;
            NavigationService = navigationService;

            usersRepository = new Repository<Users>();

            titleBorderColor = standartColor;
            UsersList = new List<Users>();

            ToNewProject = new LambdaCommand(GoNewProject);
            ToBack = new LambdaCommand(GoBack);
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
                foreach(Users user in usersRepository.Items)
                {
                    if (user.Username != currentUser.Username && user.Roles.Permissions.Where(x => x.Name == "permission_privateprojects").ToList().Any())
                    {
                        users.Add(user);
                    }
                }
                usersList = users;
            }
        }

        private void GoNewProject(object obj)
        {
            Projects newProject = new Projects();
            if (title.Length < 1 || title.Length > 50)
                return;
            newProject.Name = title;
            newProject.CreatingDate = DateTime.Now;
            newProject.User = currentUser.Id;
            newProject.Users.Add(usersRepository.Items.Where(x => x.Id == currentUser.Id).FirstOrDefault()); //Приходится делать так, потому что разные контексты
            var selectedUsers = ((IEnumerable<object>)obj).Cast<object>().ToList();
            if(selectedUsers.Count > 0)
            {
                foreach (var item in selectedUsers)
                {
                    newProject.Users.Add((Users)item);
                }
            }
            ProjectsRepository projectsRepository = new ProjectsRepository();
            projectsRepository.Add(newProject);
            NavigationService.CurrentViewModel = new ProjectListViewModel(NavigationService, currentUser);
        }
        private void GoBack(object obj)
        {
            NavigationService.CurrentViewModel = new ProjectListViewModel(NavigationService, currentUser);
        }
    }
}
