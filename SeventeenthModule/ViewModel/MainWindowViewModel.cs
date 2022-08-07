using SeventeenthModule.Models;
using SeventeenthModule.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeventeenthModule.ViewModel
{
    internal class MainWindowViewModel : ViewModels
    {
        private DataSet _tables;

        public DataSet DataBase
        {
            get { return _tables; }
            set 
            {
                if (Equals(_tables, value)) return;
                _tables = value;
                OnPropertyChanged();
                
            }
        }

        public DataTable Table0 { get; set; }



        #region Конструктор
        public MainWindowViewModel()
        {
            DataWorker dataWorker = new DataWorker();
            DataBase = dataWorker.DataSet;
        }
        #endregion

    }
}
