using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SampleCode.C3.NLogSample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoggingServiceSample" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoggingServiceSample.svc or LoggingServiceSample.svc.cs at the Solution Explorer and start debugging.
    public class LoggingServiceSample : ILoggingServiceSample
    {
        //Declare an instance of NLog for this class
        NLog.Logger mylogger = NLog.LogManager.GetCurrentClassLogger();
       
        public void LogMe(string message, string logger, string level, string callerID)
        {

            mylogger.Trace("SP / WS call started");

            NLog.LogLevel nLogLevel;

            //verify called ID
            //TO DO caller id should be decrypted before compare

            {
                if (string.Compare(callerID, "TFW123") == 0)
                {
                    //if the level cannot be recognized we will default to "Fatal"
                    try
                    {
                        nLogLevel = NLog.LogLevel.FromString(level);
                        mylogger.Log(nLogLevel, string.Concat(logger.ToUpper(), "|", message));

                    }
                    catch (Exception ex)
                    {
                        mylogger.Log(NLog.LogLevel.Fatal, string.Concat(logger.ToUpper(), " <", level, "> is not a recognized Log Level"));
                    }
                }
                else
                {
                    mylogger.Fatal(string.Concat(logger.ToUpper(), " <", callerID, "> is not authorized to call this method."));
                    //Throw exception?
                }
            }

        }
    }
}
