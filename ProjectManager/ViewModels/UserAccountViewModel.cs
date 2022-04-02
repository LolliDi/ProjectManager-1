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

namespace ProjectManager.ViewModels
{
    class UserAccountViewModel : ViewModel
    {
        private Users user;
        public UserAccountViewModel(Users user)
        {
            this.user = user;
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
    }
}
