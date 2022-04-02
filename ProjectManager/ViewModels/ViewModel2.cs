using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    class ViewModel2 : ViewModel
    {
        private string text = "Это ViewModel 2";

        public string Text
        {
            get { return text; }
            set => Set(ref text, ref value);
        }
    }
}
