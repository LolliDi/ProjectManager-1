using ProjectManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using ProjectManager.Models;
using ProjectManager.Services;
using ProjectManager.Commands;

namespace ProjectManager.ViewModels
{
    class UserAccountViewModel : ViewModel
    {
        private Users user;
        private string name = "", surname = "", patronymic = "";
        private Nullable<int> age;
        NavigationService NavigationService;
        public ICommand Back { get; set; }
        public ICommand Redact { get; set; }
        public UserAccountViewModel(Users user, NavigationService NavigationService)
        {
            this.user = user;
            this.NavigationService = NavigationService;
            if(user != null && user.Persons != null)
            {
                name = user.Persons.Name;
                surname = user.Persons.Surname;
                patronymic = user.Persons.Patronymic;
                age = user.Persons.Age;
            }            
            Back = new LambdaCommand(parameter => NavigationService.CurrentViewModel = new ProjectListViewModel(NavigationService,user));
            Redact = new LambdaCommand(RedactUser);
        }       
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }
        public Nullable<int> Age
        {
            get { return age; }
            set { age = value; }
        }
        public Users User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
            }
        }
        private void RedactUser(object obj)
        {
            Repository<Persons> repository = new Repository<Persons>();
            UsersRepository usersRepository = new UsersRepository();
            if (user.Persons != null)
            {
                user.Persons.Name = Name;
                user.Persons.Surname = Surname;
                user.Persons.Patronymic = Patronymic;
                user.Persons.Age = Age;
            }
            else
            {
                Persons person = new Persons() { Age = this.Age, Name = this.Name, Patronymic = this.Patronymic, Surname = this.Surname, Id = user.Id};               
                repository.Add(person);
            }                                    
            usersRepository.Update(user);
        }
    }
}
