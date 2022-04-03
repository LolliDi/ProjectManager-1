using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectManager.ViewModels
{
    class AddUserPageViewModel : ViewModel
    {
        static UsersRepository usersRepository = new UsersRepository(new ProjectManagerContext());
        static RolesRepository rolesRepository = new RolesRepository(new ProjectManagerContext());
        public ICommand BackPageCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public AddUserPageViewModel()
        {
            BackPageCommand = new LambdaCommand(BackPage);
            AddUserCommand = new LambdaCommand(AddUser);
            
        }

        public void BackPage(object obj)
        {
            //закрыть страницу с добавлением пользователя
        }

        public void AddUser(object obj)
        {
            string messageError = "";
            try
            {
                if (Login.Length < 1)
                {
                    messageError += "Введите логин\n";
                    
                    LoginBorderColor = Models.AddUserPageModel.redColor;
                }
                else
                {
                    LoginBorderColor = Models.AddUserPageModel.standartColor;
                }
                if (Password.Length < 1)
                {
                    messageError += "Введите пароль\n";
                    PasswordBorderColor = Models.AddUserPageModel.redColor;
                }
                else
                {
                    PasswordBorderColor = Models.AddUserPageModel.standartColor;
                }
                if (SelectedRole.Length < 1)
                {
                    messageError += "выберите роль\n";
                    RoleBorderColor = Models.AddUserPageModel.redColor;
                }
                else
                {
                    RoleBorderColor = Models.AddUserPageModel.standartColor;
                }

                if (messageError.Length > 0)
                {
                    throw new Exception(messageError);
                }

                usersRepository.Add(new Users() { Username = Login, Password = Password, Role = (rolesRepository.Items.FirstOrDefault(x => x.Name == SelectedRole)).Id });
                MessageBox.Show("Пользователь успешно добавлен.", "Вау!Ё", MessageBoxButton.OK, MessageBoxImage.Information);
                //сохранение в бд

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Пошел ты нахер, козел", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
       

        public string Name
        {
            get => Models.AddUserPageModel.name;
            set
            {
                Models.AddUserPageModel.name = value;
            }
        }
        public string LastName
        {
            get => Models.AddUserPageModel.lastName;
            set
            {
                Models.AddUserPageModel.lastName = value;
            }
        }
        public string Patronymic
        {
            get => Models.AddUserPageModel.patronymic;
            set 
            {
                Models.AddUserPageModel.patronymic = value;
            }
        }
        public string Email
        {
            get => Models.AddUserPageModel.email;
            set
            {
                Models.AddUserPageModel.email = value;
            }
        }
        public string Login
        {
            get => Models.AddUserPageModel.login;
            set
            {
                Models.AddUserPageModel.login = value;
            }
        }
        public string Password
        {
            get => Models.AddUserPageModel.password;
            set
            {
                Models.AddUserPageModel.password = value;
            }
        }
        public string SelectedRole
        {
            get => AddUserPageModel.selectedRole;
            set
            {
                AddUserPageModel.selectedRole = value;
            }
        }
        public string Age
        {
            get => ""+ AddUserPageModel.ageText;
            set
            {
                try
                {
                    if (value.Length > 0)
                    {
                        AddUserPageModel.age = int.Parse(value);
                        AddUserPageModel.ageText = value;
                    }
                    else
                    {
                        AddUserPageModel.age = 0;
                        AddUserPageModel.ageText = "";
                    }
                }
                catch
                {
                    
                }
            }
        }

        public SolidColorBrush RoleBorderColor
        {
            get => AddUserPageModel.roleBorderColor;
            set => Set(ref AddUserPageModel.roleBorderColor, ref value, "RoleBorderColor");
        }
        public SolidColorBrush LoginBorderColor
        {
            get => AddUserPageModel.loginBorderColor;
            set
            {
                Set(ref AddUserPageModel.loginBorderColor, ref value, "LoginBorderColor");
            }
        }
        public SolidColorBrush PasswordBorderColor
        {
            get => AddUserPageModel.passwordBorderColor;
            set => Set(ref AddUserPageModel.passwordBorderColor, ref value, "PasswordBorderColor");

        }

        public static List<string> DataRoles
        {
            get => AddUserPageModel.dataRoles;
        }


    }
}
