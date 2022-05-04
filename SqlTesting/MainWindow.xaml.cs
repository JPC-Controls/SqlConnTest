using SqlTesting.classes;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                _ConnectionString = value;
                OnPropertyChanged("ConnectionString");
            }
        }
        private string _ConnectionStringNP;

        public string ConnectionStringNP
        {
            get { return _ConnectionStringNP; }
            set
            {
                _ConnectionStringNP = value;
                OnPropertyChanged("ConnectionStringNP");
            }
        }

        private string _Server = @"eng-lsimoni\ohdlogic";

        public string Server
        {
            get { return _Server; }
            set
            {
                _Server = value;
                OnPropertyChanged("Server");
                UpdateConnectionString();
            }
        }
        private string _Username = "sa";

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged("Username");
                UpdateConnectionString();
            }
        }
        private string _Password = "ohdusa@123";

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
                UpdateConnectionString();
            }
        }
        private bool _ConnectionFound;

        public bool ConnectionFound
        {
            get { return _ConnectionFound; }
            set
            {
                _ConnectionFound = value;
                OnPropertyChanged("ConnectionFound");
            }
        }
        private string _Timing;

        public string Timing
        {
            get { return _Timing; }
            set
            {
                _Timing = value;
                OnPropertyChanged("Timing");
            }
        }
        private bool _CustomConfigure;

        public bool CustomConfigure
        {
            get { return _CustomConfigure; }
            set
            {
                _CustomConfigure = value;
                OnPropertyChanged("CustomConfigure");
            }
        }
        private bool _Encrypted;

        public bool Encrypted
        {
            get { return _Encrypted; }
            set
            {
                _Encrypted = value;
                OnPropertyChanged("Encrypted");
                UpdateConnectionString();
            }
        }
        private bool _UseNamedPipes;

        public bool UseNamedPipes
        {
            get { return _UseNamedPipes; }
            set
            {
                _UseNamedPipes = value;
                OnPropertyChanged("UseNamedPipes");
                UpdateConnectionString();
            }
        }

        private int _ConnTimeout = 5;

        public int ConnTimeout
        {
            get { return _ConnTimeout; }
            set
            {
                _ConnTimeout = value;
                OnPropertyChanged("ConnTimeout");
                UpdateConnectionString();
            }
        }
        public bool FailedConnection => !Untested && !ConnectionFound;

        private string _FailedConnectionInfo;

        public string FailedConnectionInfo
        {
            get { return _FailedConnectionInfo; }
            set
            {
                _FailedConnectionInfo = value;
                OnPropertyChanged("FailedConnectionInfo");
                OnPropertyChanged("FailedConnection");
            }
        }
        private bool _Untested = true;

        public bool Untested
        {
            get { return _Untested; }
            set
            {
                _Untested = value;
                OnPropertyChanged("Untested");
                OnPropertyChanged("FailedConnection");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            UpdateConnectionString();
        }
        private readonly Stopwatch sw = new();
        private void btnSDSConnect_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Untested = false;
                IsBusy = true;
                ConnectionFound = false;
                sw.Start();
                var con = new SystemDataSqlClient();
                ConnectionFound = ((FailedConnectionInfo = con.Connect(ConnectionString)) == "good");
                if (!ConnectionFound)
                {

                }
                sw.Stop();
                IsBusy = false;
                Timing = $"{sw.Elapsed.TotalSeconds:0.###}";
                sw.Reset();
            });
        }

        private void btnMDSConnect_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Untested = false;
                IsBusy = true;
                ConnectionFound = false;
                sw.Start();
                var con = new MicrosoftDataSqlClientTest();
                FailedConnectionInfo = con.Connect(ConnectionString);
                ConnectionFound = ((FailedConnectionInfo = con.Connect(ConnectionString)) == "good");
                if (!ConnectionFound)
                {

                }
                sw.Stop();
                IsBusy = false;
                Timing = $"{sw.Elapsed.TotalSeconds:0.###}";
                sw.Reset();
            });
        }

        private void UpdateConnectionString()
        {
            CreateConnectinString();
        }
        private void CreateConnectinString()
        {
            string ec = Encrypted ? "True" : "False";
            ConnectionString = $"Server={(UseNamedPipes ? "np:" + Server : Server)};User Id={Username};Password={Password};Encrypt={ec};Connection Timeout={ConnTimeout};";
            Untested = true;
            FailedConnectionInfo = String.Empty;
            ConnectionFound = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }
    }
}
