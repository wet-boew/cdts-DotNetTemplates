using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleCode.C3.NLogSample
{
    public class Person
    {
         //Declare an instance of NLog for this class
        NLog.Logger myContactClasslogger = NLog.LogManager.GetCurrentClassLogger();

        public Person() {
            myContactClasslogger.Info("A 'Person' was instantiated");
        }

        public string Name { get; set; }
    }
}