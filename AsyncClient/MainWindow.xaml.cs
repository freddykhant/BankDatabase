using BankDatabase;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.ServiceModel;
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
using System.Xml.Linq;

namespace DelegateClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public delegate DataStruct Search(string value); //delegate for searching

    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private Search search;

        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8200/BusinessService";
            foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            search = SearchDB;
            AsyncCallback callback;
            callback = this.OnSearchCompletion;
            IAsyncResult result = search.BeginInvoke(SearchBox.Text, callback, null);
        }

        private DataStruct SearchDB(string value)
        {
            uint acctNo = 0, pin = 0;
            string fName = "", lName = "";
            int bal = 0;
            foob.GetValuesForSearch(value, out acctNo, out pin, out bal, out fName, out lName);

            DataStruct account = new DataStruct();
            account.acctNo = acctNo;
            account.pin = pin;
            account.balance = bal;
            account.firstName = fName;
            account.lastName = lName;

            return account;
        }

        private void UpdateGui(DataStruct account)
        {
            FNameBox.Dispatcher.Invoke(new Action(() => FNameBox.Text = account.firstName));
            LNameBox.Dispatcher.Invoke(new Action(() => LNameBox.Text = account.lastName));
            AcctNoBox.Dispatcher.Invoke(new Action(() => AcctNoBox.Text = account.acctNo.ToString()));
            PinBox.Dispatcher.Invoke(new Action(() => PinBox.Text  = account.pin.ToString()));
            BalanceBox.Dispatcher.Invoke(new Action(() => BalanceBox.Text = "$" + account.balance.ToString()));
        }

        private void OnSearchCompletion(IAsyncResult asyncResult)
        {
            DataStruct iAccount = null;
            Search search = null;
            AsyncResult asyncobj = (AsyncResult)asyncResult;
            if (asyncobj.EndInvokeCalled == false)
            {
                search = (Search)asyncobj.AsyncDelegate;
                iAccount = search.EndInvoke(asyncobj);
                UpdateGui(iAccount);
            }
            asyncobj.AsyncWaitHandle.Close();
        }
    }
}
