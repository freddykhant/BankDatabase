using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDatabase
{
    public class DatabaseClass
    {
        private List<DataStruct> dataStruct;

        public DatabaseClass()
        {
            dataStruct = new List<DataStruct>();
            InitializeDatabase(100000); // You can change the number of entries as needed
        }

        private void InitializeDatabase(int numEntries)
        {
            DatabaseGenerator generator = new DatabaseGenerator();

            for (int i = 0; i < numEntries; i++)
            {
                DataStruct record = new DataStruct
                {
                    acctNo = generator.GetAcctNo(),
                    pin = generator.GetPIN(),
                    balance = generator.GetBalance(),
                    firstName = generator.GetFirstname(),
                    lastName = generator.GetLastname(),
                };

                dataStruct.Add(record);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            if (index < 0 || index >= dataStruct.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return dataStruct[index].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            if (index < 0 || index >= dataStruct.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return dataStruct[index].pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            if (index < 0 || index >= dataStruct.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return dataStruct[index].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            if (index < 0 || index >= dataStruct.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return dataStruct[index].lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            if (index < 0 || index >= dataStruct.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            return dataStruct[index].balance;
        }

        public int GetNumEntries()
        {
            return dataStruct.Count;
        }

        public List<DataStruct> GetRecords()
        {
            return dataStruct;
        }
    }
}
