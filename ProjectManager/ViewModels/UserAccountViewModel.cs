using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ProjectManager.Models;

namespace ProjectManager.ViewModels
{
    class UserAccountViewModel : ViewModel
    {

        private List<Users> users = new List<Users>();
        private List<Persons> persons = new List<Persons>();
        private CollectionViewSource userDataViewSource;

        public ICollectionView UserCollectionView => userDataViewSource.View;
    }
}
