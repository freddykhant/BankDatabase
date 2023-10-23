using BankDatabase;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Async
{
    public delegate DataStruct Search(string value); //delegate for searching
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private string searchvalue = "";
        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8200/BusinessService";
            foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchvalue = SearchBox.Text;
            Task<DataStruct> task = new Task<DataStruct>(SearchDB);
            task.Start();
            statusLabel.Content = "Searching starts.....";
            DataStruct acc = await task;
            UpdateGui(acc);
            statusLabel.Content = "Searching ends.....";
        }

        private DataStruct SearchDB()
        {
            uint acctNo = 0, pin = 0;
            string fName = "", lName = "";
            int bal = 0;
            foob.GetValuesForSearch(searchvalue, out acctNo, out pin, out bal, out fName, out lName);

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
            FNameBox.Text = account.firstName;
            LNameBox.Text = account.lastName;
            AcctNoBox.Text = account.acctNo.ToString();
            PinBox.Text = account.pin.ToString();
            BalanceBox.Text = "$" + account.balance.ToString();
        }
    }
}
