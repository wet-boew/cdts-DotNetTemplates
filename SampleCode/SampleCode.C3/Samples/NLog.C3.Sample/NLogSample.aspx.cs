using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleCode.C3.NLogSample
{
    public partial class NLogSample : System.Web.UI.Page
    {
        //Declare an instance of NLog for this class
        NLog.Logger mylogger = NLog.LogManager.GetCurrentClassLogger();

        protected void btnCreateLog_Click(object sender, EventArgs e)
        {
            //log start time of execution
            mylogger.Trace(string.Concat("btnLog_Click process started at: ", DateTime.Now));

            //Start a Stopwatch to calculate process duration
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                
                //Declare person class
                Person person = new Person();
                person.Name = "Billy";

                //Log all level types
                mylogger.Fatal("I'm logging - FATALS");
                mylogger.Error("I'm logging - ERRORS");
                mylogger.Warn("I'm logging - WARNINGS");
                mylogger.Info("I'm logging - INFO");
                mylogger.Debug("I'm logging  - DEBUG");
                mylogger.Trace("I'm logging - TRACE");

                //throw an exception
                throw new ArgumentException();
            }
            catch (Exception ex)
            {
                //log any exception that occurs and the one that we raised
                //shows the ".Log" method where the level is supplied as a parameter
                mylogger.Log(NLog.LogLevel.Error, ex);
            }
            finally
            {
                //log end time of execution
                mylogger.Trace(string.Concat("btnLog_Click process ended at: ", DateTime.Now));
                watch.Stop();
                long elapseMS = watch.ElapsedMilliseconds;

                //log stopwatch elapse time
                mylogger.Info(String.Concat("btnLog_Click process duration: ", elapseMS, " ms"));

                //log a warning should the elapse time be greater then my accepted limit. (limit should be stored in configuration file)
                if (elapseMS > 200)
                {
                    mylogger.Warn(string.Concat("btnLog_Click process exceeded the accepted elapse time, taking ", elapseMS, " ms to execute"));
                }
            }
        }
    }    
}