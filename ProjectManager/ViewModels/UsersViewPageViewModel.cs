using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class UsersViewPageViewModel : ViewModel
    {
        public ICommand OpenUserCommand { get; set; }
        public ICommand DelUserCommand { get; set; }
        public ICommand AddUser { get; set; }
        

        int selectedIndex = 0;

        List<UsersInfo> users = new List<UsersInfo>();
        public FormNavigationService AddUserPageForm { get; set; }

        public UsersViewPageViewModel(NavigationService NavigationService)
        {
            AddUser = new LambdaCommand(OnAddUserCommandExecuted);
            OpenUserCommand = new LambdaCommand(OpenUser);
            DelUserCommand = new LambdaCommand(DelUser);
            AddUserPageForm = new FormNavigationService() { OnClosingFormCallback = OnTaskFormClosing };
            foreach (Users user in new Repository<Users>().Items.ToList())
            {
                users.Add(new UsersInfo(user));
            }
        }

        private void OnAddUserCommandExecuted(object parameter)
        {
            AddUserPageForm.IsOpen = true;
            AddUserPageForm.CurrentViewModel = new AddUserPageViewModelForm(AddUserPageForm) {Header = "Довавление пользователя"};
        }
        private void OnTaskFormClosing()
        {
            RefreshTasksCollection();
        }

        private void RefreshTasksCollection()
        {
            Users = null;
        }

        public void DelUser(object obj)
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    if (MessageBox.Show
                        (
                        "Вы действительно хотите удалить " + users[selectedIndex].Login + "?", "Подтвердите",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                        )
                        == MessageBoxResult.Yes)
                    {
                        new Repository<Users>().Remove(users[selectedIndex].Id);
                    }
                    Users = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<UsersInfo> Users
        {
            get => users;
            set
            {
                List<UsersInfo> nusers = new List<UsersInfo>();
                foreach (Users user in new Repository<Users>().Items.ToList())
                {
                    nusers.Add(new UsersInfo(user));
                }
                users.Clear();
                Set(ref users, ref nusers);
            }
        }

        public void OpenUser(object obj)
        {
            //открываем страницу редактирования
        }



        public struct UsersInfo
        {
            int id;
            string login;
            string userInfo;
            public UsersInfo(Users us)
            {
                id = us.Id;
                login = us.Username;
                userInfo = "Нет данных";
                try
                {
                    if (us.Persons != null)
                    {
                        userInfo = "";
                        if (us.Persons.Surname != "")
                        {
                            userInfo += us.Persons.Surname + " ";
                        }
                        if (us.Persons.Name != "")
                        {
                            userInfo += us.Persons.Name + " ";
                        }
                        if (us.Persons.Patronymic != "")
                        {
                            userInfo += us.Persons.Patronymic + " ";
                        }                       
                        if (us.Persons.Age !=null)
                        {
                            userInfo += "(" + us.Persons.Age+ " лет)";
                        }
                    }
                }
                catch
                {

                }
            }

            public string Login
            {
                get => login;
                set => login = value;
            }
            public string UserInfo
            {
                get => userInfo;
                set => userInfo = value;
            }
            public int Id
            {
                get => id;
                set => id = value;
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set => selectedIndex = value;
        }

    }
}
