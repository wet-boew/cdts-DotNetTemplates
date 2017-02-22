using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SampleCode.C3.NLogSample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoggingServiceSample" in both code and config file together.
    [ServiceContract]
    public interface ILoggingServiceSample
    {
        [OperationContract (Action = "http://tempuri.org/LogMe")]
        void LogMe(string message, string logger, string level, string callerID);
    }
}
