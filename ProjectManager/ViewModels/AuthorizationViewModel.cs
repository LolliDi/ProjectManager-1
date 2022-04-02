using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    class AuthorizationViewModel
    {

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


    }
}
