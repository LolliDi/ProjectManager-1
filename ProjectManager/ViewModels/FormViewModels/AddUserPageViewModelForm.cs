﻿using ProjectManager.Commands;
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
    class AddUserPageViewModelForm : FormViewModel
    {
        static UsersRepository usersRepository = new UsersRepository(); //пользователи
        static Repository<Roles> rolesRepository = new Repository<Roles>();
        public ICommand BackPageCommand { get; set; } //для кнопки отмена
        FormNavigationService NavigationService;



        public AddUserPageViewModelForm(FormNavigationService NavigationService) : base(NavigationService)
        {
            this.NavigationService = NavigationService;
            NullMainInf();
        }

        protected override void OnSubmitCommandExecute(object parameter)
        {
            if (AddUser())
            {
                base.OnSubmitCommandExecute(parameter);
            }
            else
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Properties
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

        public string Name //далее свойства для работы с данными и цветами
        {
            get => AddUserPageModel.name;
            set
            {
                AddUserPageModel.name = value;
            }
        }
        public string LastName
        {
            get => AddUserPageModel.lastName;
            set
            {
                AddUserPageModel.lastName = value;
            }
        }
        public string Patronymic
        {
            get => AddUserPageModel.patronymic;
            set
            {
                AddUserPageModel.patronymic = value;
            }
        }
        public string Email
        {
            get => AddUserPageModel.email;
            set
            {
                AddUserPageModel.email = value;
            }
        }
        public string Login
        {
            get => AddUserPageModel.login;
            set
            {
                AddUserPageModel.login = value;
            }
        }
        public string Password
        {
            get => AddUserPageModel.password;
            set
            {
                AddUserPageModel.password = value;
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
        #endregion

        #region Methods       
        public void CheckErrorText(ref string messageError, string checkString, short id, string error) //проверка на пустоту строки + меняем цвет обязательных полей,
        {                                                                                               //если они пустые
            SolidColorBrush red = AddUserPageModel.redColor;
            SolidColorBrush gray = AddUserPageModel.standartColor;
            if (checkString.Length < 1)
            {
                messageError += error + "\n";
                switch (id)
                {
                    case 0:
                        LoginBorderColor = red;
                        return;
                    case 1:
                        PasswordBorderColor = red;
                        return;
                    case 2:
                        RoleBorderColor = red;
                        return;
                    default: break;
                }
            }
            else
            {
                switch (id)
                {
                    case 0:
                        LoginBorderColor = gray;
                        return;
                    case 1:
                        PasswordBorderColor = gray;
                        return;
                    case 2:
                        RoleBorderColor = gray;
                        return;
                    default: break;
                }
            }
        }

        public void NullMainInf()
        {
            Login = "";
            Password = "";
            Name = "";
            LastName = "";
            Patronymic = "";
            Age = "";
            SelectedRole = "";
        }


        public bool AddUser()
        {
            string messageError = ""; //сообщение ошибки, будет заполняться по мере появления ошибок
            try
            {
                CheckErrorText(ref messageError, Login, 0, "Введите логин"); //проверка обязательных данных
                CheckErrorText(ref messageError, Password, 1, "Введите пароль");
                CheckErrorText(ref messageError, SelectedRole, 2, "Выберите роль");

                if (messageError.Length > 0) //выбиваем ошибку, если что то не заполнено
                {
                    return false;                                    
                }


                usersRepository.Add(new Users() { Username = Login, Password = Password, Role = (rolesRepository.Items.FirstOrDefault(x => x.Name == SelectedRole)).Id }); //бобавили основные данные

                if (Name.Length > 0 || LastName.Length > 0 || Patronymic.Length > 0 || Age.Length > 0) //если есть дополнительные данные
                {
                    int idUser = usersRepository.Items.FirstOrDefault(x => x.Username == Login).Id; //получаем ид этого усера
                    Repository<Persons> person = new Repository<Persons>();
                    person.Add(new Persons() { Age = Convert.ToInt32(Age), Name = Name, Patronymic = Patronymic, Surname = LastName, Id = idUser });
                }

                MessageBox.Show("Пользователь успешно добавлен.", "Вау!Ё", MessageBoxButton.OK, MessageBoxImage.Information);
                


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Пошел ты нахер, козел", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        public string Age // ввод возраста с обработкой ввода символов
        {
            get => "" + AddUserPageModel.ageText;
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
        #endregion        

    }
}
