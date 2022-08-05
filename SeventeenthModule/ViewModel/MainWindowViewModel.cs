using SeventeenthModule.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventeenthModule.ViewModel
{
    internal class MainWindowViewModel : ViewModels
    {
        private string myVar ="123";

        public string MyProperty 
        {
            get { return myVar; }
            set 

            {
                if (Equals(myVar, value)) return;

                myVar = value;

                OnPropertyChanged();
            
            }
        }

    }
}
