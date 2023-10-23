using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BankDatabase;

namespace DatabaseServer
{
    [ServiceContract]
    public interface DataServerInterface
    {
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        [FaultContract(typeof(ErrorData))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string firstName, out string lastName);

        [OperationContract]
        List<DataStruct> GetAllRecords();
    }

    [DataContract]
    public class ErrorData
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
