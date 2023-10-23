using Business;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;

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
            try
            {
                uint acctNo = 0, pin = 0;
                string fName = "", lName = "";
                int bal = 0;
                foob.GetValuesForSearch(SearchBox.Text, out acctNo, out pin, out bal, out fName, out lName);
                AcctNoBox.Text = acctNo.ToString();
                PinBox.Text = pin.ToString("D4");
                BalanceBox.Text = "$" + bal.ToString("C");
                FNameBox.Text = fName;
                LNameBox.Text = lName;

            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter a valid number for the index.");
            }
            catch (FaultException<ErrorData> ex)
            {
                MessageBox.Show($"Server Error: {ex.Detail.ErrorMessage}");
            }
            catch (Exception ex)
            {
                var errorData = new ErrorData { ErrorMessage = ex.Message };
                throw new FaultException<ErrorData>(errorData, "An error occurred while processing your request.");
            }
        }
    }
}
