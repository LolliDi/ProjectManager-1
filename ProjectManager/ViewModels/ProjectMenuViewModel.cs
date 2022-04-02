using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.ViewModels
{
    class ProjectMenuViewModel : ViewModel
    {
        public ProjectMenuViewModel(List<Projects> projectList)
        {
            this.projectList = projectList;
        }

        private List<Projects> projectList;
        public List<Projects> ProjectList
        {
            get => projectList;
            set => projectList = value;
        }

    }
}
