﻿using ProjectManager.Commands;
using ProjectManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class AuthorizationViewModel : ViewModel
    {
        public AuthorizationViewModel()
        {
            AutoCommand = new LambdaCommand(Verefication);
        }

        UsersRepository usersRepository = new UsersRepository(new Models.ProjectManagerContext());
        public ICommand AutoCommand { get; set; }

        string login, pass;
        public string GetLogin
        {
            set
            {
                login = value;
            }
        }

        public string GetPass
        {
            set
            {
                pass = value;
            }
        }

        public void Verefication(object obj)
        {
            if (login != null || pass != null)
            {
                var user = usersRepository.Items.FirstOrDefault(x => x.Username == login && x.Password == pass);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var permisions = user.Roles.Permissions;
                    if (permisions.Where(x => x.Id == 1).Any())
                    {
                        //вас кидает к админу
                    }
                    else 
                    { 
                        //в основное меню
                    }
                }
            }
            else 
            {
                MessageBox.Show("Есть пусты строки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
