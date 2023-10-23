using BankDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class DataServer : DataServerInterface
    {
        private DatabaseClass database;

        public DataServer()
        {
            database = new DatabaseClass();
        }

        public int GetNumEntries()
        {
            return database.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string firstName, out string lastName)
        {
            if (index >= 0 && index < database.GetNumEntries())
            {
                acctNo = database.GetAcctNoByIndex(index);
                pin = database.GetPINByIndex(index);
                bal = database.GetBalanceByIndex(index);
                firstName = database.GetFirstNameByIndex(index);
                lastName = database.GetLastNameByIndex(index);
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range");
            }
        }

        public List<DataStruct> GetAllRecords()
        {
            return database.GetRecords();
        }
    }
}
