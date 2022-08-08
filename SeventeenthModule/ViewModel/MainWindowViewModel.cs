using SeventeenthModule.Infrastructure;
using SeventeenthModule.Models;
using SeventeenthModule.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SeventeenthModule.ViewModel
{
    internal class MainWindowViewModel : ViewModels
    {

        #region Поля и свойства

        #region Private поля

        private DataSet _tables;
        private string _fname;
        private string _lname;
        private string _phone;
        private string _emai;
        private string _pName;

        #endregion

        #region Основные свойства
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

        public SqlCommands Sql { get; set; }


        #endregion

        #region Свойства команды добавления клиента

        public string Fname
        {
            get => _fname;
            set
            {
                if (Equals(_fname, value)) return;
                _fname = value;
                OnPropertyChanged();

            }
        }     

        public string Lname
        {
            get => _lname;
            set
            {
                if (Equals(_lname, value)) return;
                _lname = value;
                OnPropertyChanged();

            }
        }
        public string Pname
        {
            get { return _pName; }
            set
            {
                if (Equals(_pName, value)) return;
                _pName = value;
                OnPropertyChanged();

            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (Equals(_phone, value)) return;
                _phone = value;
                OnPropertyChanged();

            }
        }

        public string Emai
        {
            get { return _emai; }
            set
            {
                if (Equals(_emai, value)) return;
                _emai = value;
                OnPropertyChanged();

            }
        }

        #endregion

        #endregion


        #region Команды

        #region Команда добавления клиента

        public ICommand AddsClientCommand { get; }

        private bool CanAddsClientExecute(object p)
        {
            return true;
        }

        private void OnAddsClientExecuted(object p)
        {
            Sql.Istert(Fname, Lname, Pname, Phone, Emai, DataBase.Tables["Clients"]);
        }

        #endregion

        #endregion


        #region Конструктор
        public MainWindowViewModel()
        {
            DataWorker dataWorker = new DataWorker();
            Sql = new SqlCommands();

            DataBase = dataWorker.DataSet;
            

            AddsClientCommand = new LamdaCommand(OnAddsClientExecuted, CanAddsClientExecute);
        }
        #endregion

    }
}
