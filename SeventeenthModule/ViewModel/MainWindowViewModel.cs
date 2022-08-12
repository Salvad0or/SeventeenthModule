using SeventeenthModule.Infrastructure;
using SeventeenthModule.Models;
using SeventeenthModule.Services;
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
        private int[] _mass;
        private int _id;
        private int _clientidforaddorder;
        private Client _sqlclient;
        private int _deleteid;
        private JoinCommands _join;
        private InsertCommands _insertCommand;
        #endregion

        #region Основные свойства


        DataWorker dataWorker { get; set; }
        SelectCommands selectCommands { get; set; }

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

        public SecondWindowWorker SqlCommand { get; set; }

       
        public InsertCommands InsertCommand
        {
            get { return _insertCommand; }
            set { _insertCommand = value; }
        }



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

        #region Свойства поиска клиента по ID

        public int Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value)) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public Client SqlClient
        {
            get { return _sqlclient; }
            set
            {
                _sqlclient = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства удаления клиента по ID

        public int DeleteId
        {
            get { return _deleteid; }
            set
            {             
                _deleteid = value;

                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства добавления нового заказа

        

        public int ClientIdForAddOrder
        {
            get { return _clientidforaddorder; }
            set 
            {
                _clientidforaddorder = value;
                OnPropertyChanged();
            }
        }



        public int [] Mass
        {
            get => _mass;
            set

            {
                _mass = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойсвта окна JOIN


        /// <summary>
        /// Основной экземляр класса Join
        /// </summary>
        public JoinCommands Join
        {
            get { return _join; }
            set 
            {
                _join = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Свойство для поиска клиента по ID или телефону
        /// </summary>
        public int JoinId { get; set; }


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
            SqlCommand.Istert(Fname, Lname, Pname, Phone, Emai, DataBase.Tables["Clients"]);
        }

        #endregion

        #region Команда поиска клиента для редактирования по ID

       


        public ICommand SearchClientByIdForEditingCommand { get; }

        public bool CanSearchClientByIdForEditingExecuted(object p) => true;
        public void OnSearchClientByIdForEditingExecute(object p)
        {
            SqlClient = SqlCommand.SearchClientsById(SqlClient, Id);
        }


        #endregion

        #region Команда изменения данных о клиенте

        public ICommand EditClientDataCommand { get; }


        public bool CanEditClientDataExecute(object p) => true;

        public void OnEditClientDataExecute(object p)
        {
            SqlCommand.EditClientData(SqlClient, Id, DataBase.Tables["Clients"]);
        }

        #endregion

        #region Команда удаления клиента

        public ICommand DeleteClientCommand { get; }

        public bool CanDeleteClientExecuted(object p) => true;

        public void OnDeleteCleientExecute (object p)
        {
            SqlCommand.DeleteClient(DeleteId, DataBase.Tables["Clients"]);
            DeleteId = 0;
        }
        #endregion

        #region Команда соединения клиента по покупкам

        private DataTable _t;
        public DataTable Table 
        { 
            get => _t;
            
            set
            {
                _t = value;
                OnPropertyChanged();
            }
        }

        public ICommand JoinClientByIdCommand { get; }

        public bool CanJoinClientByIdExecuted(object p) => true;

        public void OnJoinClientByIdExecuted(object p)
        {
            Join.JoinTableById(JoinId);
        }

        #endregion

        #region Команда добавления нового заказа

        public  ICommand AddOrderCommand { get; }

        public bool CanAddOrderExecuted(object p) => true;

        public void OnAddOrderExecute(object p)
        {
            InsertCommand.InsertDataInOrders(Mass,ClientIdForAddOrder, Join);
        }

        #endregion

        #endregion


        #region Конструктор
        public MainWindowViewModel()
        {
            #region инициализация всех объектов

            dataWorker = new DataWorker(ShowMessages);
            selectCommands = new SelectCommands();    
            SqlCommand = new SecondWindowWorker();
            SqlClient = new Client();
            Join = new JoinCommands();
            Mass = new int[5];
            InsertCommand = new InsertCommands();
            Table = new DataTable();

            DataBase = selectCommands.DataSet;

            #endregion

            #region Объявление команд

            AddsClientCommand = new LamdaCommand(OnAddsClientExecuted, CanAddsClientExecute);
            SearchClientByIdForEditingCommand = new LamdaCommand(OnSearchClientByIdForEditingExecute, CanSearchClientByIdForEditingExecuted);
            EditClientDataCommand = new LamdaCommand(OnEditClientDataExecute, CanEditClientDataExecute);
            DeleteClientCommand = new LamdaCommand(OnDeleteCleientExecute, CanDeleteClientExecuted);
            JoinClientByIdCommand = new LamdaCommand(OnJoinClientByIdExecuted, CanJoinClientByIdExecuted);
            AddOrderCommand = new LamdaCommand(OnAddOrderExecute, CanAddOrderExecuted);

            #endregion
        }
        #endregion

        #region Вспомогательные методы

        public void ShowMessages(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

    }
}
