using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    class ViewModel1 : ViewModel
    {
        private string text = "Это ViewModel 1";

        public string Text
        {
            get => text;
            set => Set(ref text, ref value);
        }
    }
}
