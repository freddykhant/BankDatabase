using DatabaseServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class BusinessServer : BusinessServerInterface
    {
        private DataServerInterface foob;
        public BusinessServer()
        {
            ChannelFactory<DataServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        public int GetNumEntries()
        {
            return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName);
        }


        public void GetValuesForSearch(string search, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            int numEntry = foob.GetNumEntries();

            for (int i = 1; i <= numEntry; i++)
            {
                uint sAcctNo, sPin;
                int sBal;
                string sFName, sLName;
                foob.GetValuesForEntry(i, out sAcctNo, out sPin, out sBal, out sFName, out sLName);
                if (sLName.ToLower().Contains(search.ToLower()))
                {
                    acctNo = sAcctNo;
                    pin = sPin;
                    bal = sBal;
                    fName = sFName;
                    lName = sLName;
                    break;
                }
            }
        }
    }
}
