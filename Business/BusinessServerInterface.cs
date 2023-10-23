using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);

        [OperationContract]
        void GetValuesForSearch(string search, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
    }

    [DataContract]
    public class ErrorData
    {
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
