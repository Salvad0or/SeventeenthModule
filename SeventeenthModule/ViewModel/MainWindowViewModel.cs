﻿using Microsoft.EntityFrameworkCore;
using SeventeenthModule.EntityObjects;
using SeventeenthModule.Infrastructure;
using SeventeenthModule.Models;
using SeventeenthModule.Services;
using SeventeenthModule.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        #region Делегаты

        /// <summary>
        /// Делегат отчистки классов которые привязаны к каким либо текст боксам.
        /// </summary>
        /// <returns></returns>
        public delegate EntityClient ClearData();
        ClearData Clear;

        #endregion

        #region Private поля

        
        private JoinCommands _join;
        private InsertCommands _insertCommand;
        private EntityClient _sqlclient;
        private EntityClient _newclient;
        private List<EntityClient> _clients;
        private List<Order> _orders;
        private List<Product> _products;
        private List<JoinCommands> _allOrdersTable;

        private List<JoinCommands> _joinbyid;
        private string _fname;
        private string _lname;
        private string _phone;
        private string _emai;
        private string _pName;
        private int[] _mass;
        private int _id;
        private int _clientidforaddorder;
        private int _deleteid;
        private string _tablename;


        #endregion

        #region Основные свойства

        #region Коллекции хранящие сущности таблиц базы
        public List<EntityClient> Clients
        {
            get => _clients;

            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
                
            }
        }

        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
                
            }
        }

        #endregion

        DataWorker dataWorker { get; set; }

        SelectCommands SqlSelect { get; set; }

        UpdateCommands SqlUpdate { get; set; }

        DeleteCommands SqlDelete { get; set; }

       

        public JoinCommands SqlJoin
        {
            get { return _join; }
            set
            {
                _join = value;
                OnPropertyChanged();
            }
        }


        public InsertCommands SqlInsert
        {
            get { return _insertCommand; }
            set { _insertCommand = value; }
        }

        CheckService Check { get; set; }



        #endregion

        #region Свойства команды добавления клиента

        public EntityClient NewClient
        {
            get => _newclient;
            set
            {
                _newclient = value;
                OnPropertyChanged();
            }
        }
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

        public EntityClient ClientWichWillChange
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


        /// <summary>
        /// Айди клиента которому додавляют новый заказ
        /// </summary>
        public int ClientIdForAddOrder
        {
            get { return _clientidforaddorder; }
            set
            {
                _clientidforaddorder = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Массив для Id заказов
        /// </summary>
        public int[] Mass
        {
            get => _mass;
            set

            {
                _mass = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойства окна JOIN

        /// <summary>
        /// Свойство для поиска клиента по ID или телефону
        /// </summary>
        public int JoinId { get; set; }
       
        public List<JoinCommands> JoinById
        {
            get => _joinbyid;
            set
            {
                _joinbyid = value;
                OnPropertyChanged();
            }
        }


        
        public List<JoinCommands> AllOrdersTable
        {
            get => _allOrdersTable;
            set
            {
                _allOrdersTable = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойства окна удаления таблиц

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName
        {
            get => _tablename;
            set
            {
                _tablename = value;
                OnPropertyChanged();
            }
        }

        string[] TableNames { get; set; }


        #endregion

        #endregion

        #region Команды

        #region Команда добавления клиента

        public ICommand AddsClientCommand { get; }

        private bool CanAddsClientExecute(object p) => Check.CheckOnEmpty(NewClient);


        private void OnAddsClientExecuted(object p)
        {
            Clients = SqlInsert.IstertNewClient(NewClient, Clients);
            NewClient = Clear?.Invoke();
        }
            

        #endregion

        #region Команда поиска клиента для редактирования по ID

        public ICommand SearchClientByIdForEditingCommand { get; }

        public bool CanSearchClientByIdForEditingExecuted(object p) => Check.CheckClientId(Id);
        
        public void OnSearchClientByIdForEditingExecute(object p)
        {
            ClientWichWillChange = SqlSelect.SearchClientsById(ClientWichWillChange, Id);
        }


        #endregion

        #region Команда изменения данных о клиенте

        public ICommand EditClientDataCommand { get; }


        public bool CanEditClientDataExecute(object p) => Check.CheckOnEmpty(ClientWichWillChange);

        public void OnEditClientDataExecute(object p)
        {
            Clients = SqlUpdate.EditClientData(Id, ClientWichWillChange, Clients);
            ClientWichWillChange = Clear?.Invoke();
            Id = default;
        } 

        #endregion

        #region Команда удаления клиента

        public ICommand DeleteClientCommand { get; }

        public bool CanDeleteClientExecuted(object p) => Check.CheckClientId(DeleteId);

        public void OnDeleteCleientExecute (object p)
        {
            Clients = SqlDelete.DeleteClient(DeleteId, Clients);
            DeleteId = 0;
        }
        #endregion

        #region Команда соединения клиента по покупкам

        public ICommand JoinClientByIdCommand { get; }

        public bool CanJoinClientByIdExecuted(object p) => true;//Check.CheckClientId(JoinId);

       

        public void OnJoinClientByIdExecuted(object p)
        {
            JoinById = SqlJoin.JoinTableById(JoinId);
        }

        #endregion

        #region Команда добавления нового заказа

        public  ICommand AddOrderCommand { get; }

        public bool CanAddOrderExecuted(object p)
        {
            //var count = DataBase.Tables["Products"].Rows;

            //for (int i = 0; i < Mass.Length; i++)
            //{
            //    if (Mass[i] <= 0 || Mass[i] > count.Count) return false;
            //}


            return true;
            //return Check.CheckClientId(ClientIdForAddOrder);
        } 

        public void OnAddOrderExecute(object p)
        {
            SqlInsert.InsertDataInOrders(Mass,ClientIdForAddOrder);
            AllOrdersTable = SqlJoin.InitializeJoinTableByAllOrders();
        }

        #endregion

        #region Команда очистки данных из таблиц

        public ICommand DeleteEvrethingFromTableCommand { get; }

        public bool CanDeleteEvrethingFromTableExecuted(object p) => Check.CheckTableName(TableName);

        public void OnDeleteEvrethingFromTableExecute(object p)
        {
            SqlDelete.DeleteTable(TableName);
            TableName = String.Empty;
        }

        #endregion

        #endregion

        #region Конструктор
        public MainWindowViewModel()
        {
            #region инициализация всех объектов

            using (Context context = new Context())
            { 
                Clients = context.Clients.ToList();
                Orders = context.Orders.ToList();
                Products = context.Products.ToList();
            }

            dataWorker = new DataWorker(ShowMessages);

            SqlSelect = new SelectCommands();           
            SqlInsert = new InsertCommands();
            SqlUpdate = new UpdateCommands();
            SqlDelete = new DeleteCommands();
            SqlJoin = new JoinCommands();

            AllOrdersTable = SqlJoin.InitializeJoinTableByAllOrders();
            Clear = ClearMethod;

            NewClient = new EntityClient();
            ClientWichWillChange = new EntityClient();
            Check = new CheckService();

            Mass = new int[5];           
                      
            #endregion

            #region Объявление команд

            AddsClientCommand = new LamdaCommand(OnAddsClientExecuted, CanAddsClientExecute);
            SearchClientByIdForEditingCommand = new LamdaCommand(OnSearchClientByIdForEditingExecute, CanSearchClientByIdForEditingExecuted);
            EditClientDataCommand = new LamdaCommand(OnEditClientDataExecute, CanEditClientDataExecute);
            DeleteClientCommand = new LamdaCommand(OnDeleteCleientExecute, CanDeleteClientExecuted);
            JoinClientByIdCommand = new LamdaCommand(OnJoinClientByIdExecuted, CanJoinClientByIdExecuted);
            AddOrderCommand = new LamdaCommand(OnAddOrderExecute, CanAddOrderExecuted);
            DeleteEvrethingFromTableCommand = new LamdaCommand(OnDeleteEvrethingFromTableExecute, CanDeleteEvrethingFromTableExecuted);

            #endregion
        }
     
        #endregion

        #region Вспомогательные методы

        public void ShowMessages(string message)
        {
            MessageBox.Show(message);
        }

        public EntityClient ClearMethod()
        {
            EntityClient c = new EntityClient();
            return c;
        }

        #endregion

    }
}
