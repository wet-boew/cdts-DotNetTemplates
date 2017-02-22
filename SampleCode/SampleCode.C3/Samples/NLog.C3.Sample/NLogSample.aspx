<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NLogSample.aspx.cs" Inherits="SampleCode.C3.NLogSample.NLogSample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NLog Sample</title>
    <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <h1>NLog Sample Code</h1>
        </div>
         <div>
            <asp:Button ID="btnCreateLog" runat="server" Text="Create Log Entry" OnClick="btnCreateLog_Click" />
        </div>
        <div>
            <h2>About NLog</h2>
            <p><a href="http://nlog-project.org/" target="_blank">NLog</a> is a free logging platform for .NET, Xamarin, Silverlight and Windows Phone with rich log routing and management capabilities. NLog makes it easy to produce and manage high-quality logs for your application regardless of its size or complexity. 
            NLog can process diagnostic messages emitted from any .NET language (C#, VB.NET etc.), augment them with contextual information (date and time, severity, thread, process, environment), format according to your preferences and send to one or more targets.</p>
             <p>Nlog supports the following log levels (in descending order)
                <ul>
                    <li>Fatal: Highest level</li>
                    <li>Error: Application crashes / exceptions</li>
                    <li>Warn: Incorrect behavior but the application can continue</li>
                    <li>Info: Normal behavior like mail sent, user updated profile etc</li>
                    <li>Debug: Executed queries, user authenticated, session expired</li>
                    <li>Trace: Begin method X, end method X etc</li>
                </ul>
            </p>
        </div>
        <div>
            <h2>About this sample</h2>
            <p>The sample is in support of the <a href="http://architecture/SF-ML/CoP/CT%20%20TA/Workshop%20Application%20Logging.aspx">Application Logging Recommendation</a> and is intended to provide you with a working example on how to use the basic functionalities of NLog.</p>
            <ul>
                <li>It assumes that you have basic understanding of NLog.</li>
                <li>It demonstrates writing to a file, a Web Service and to an Oracle Database via a stored procedure.</li>
                <li>It follows the naming convention for files as outline in the recommendation </li>
                <li>It logs the following information: date, machinename, message level, logger name, threadid, message</li>
                <li>The log files generated are delimited with a "|", so they can be imported to excel to provide filtering and sorting capabilities.</li>
                <li>It demonstrates the use of <a href="https://github.com/nlog/NLog/wiki/Configuration-file#targets">Targets</a> and <a href="https://github.com/nlog/NLog/wiki/Configuration-file#rules">Rules</a>.</li>
                <li>It demonstrates the use of <a href="https://github.com/nlog/NLog/wiki/FallbackGroup-target">Fallback Targets</a> should your original target fail.</li>
                <li>Visit <a href="http://nlog-project.org/">NLog</a> for more details, configurations and extensibility of NLog to meet your needs.</li>
            </ul>
            <p>The sample includes:</p>
            <ul>
                <li>"NLogSample.aspx": A web page to launch the sample and create the log entry</li>
                <li>"Person.cs": A class used to simulate mulitple objects in your projects and demonstrate different logging scenarios</li>
                <li>"LoggingServiceSample.svc": A WCF service used to demonstrate logging to a service</li>
                <li>"NLog.Config.Scenarios": A file containing the nlog config entries you will use to detemontrate the NLog functionalities.  You will copy entries from this file to your local NLog.Config file.</li>
                <li>"DBSCript": A DB script to create the table and package used in this sample.</li>
            </ul>
         </div>
        <div>
            <h2>Pre-Requisites</h2>
            <p>The latest version of NLog.  NLog and its configuration file will need to be installed in the same project you have installed this sample code. NLog can be downloaded via NuGet, from NuGet.org, package name = "Nlog.Configuration".</p>
        </div>
        <div>
            <h2>How to use this sample</h2>
            <ul>
                <li>Make "NLogSample.aspx" your start up page.</li>    
                <li>Copy one of the Nlog configuration scenarios to your nlog.config
                    <ul>
                        <li>Open the Nlog.Config file of your project</li>
                        <li>Remove or comment out any "targets" or "rules" in it.</li>
                        <li>Copy one of the nlog configuration scenarios from the "NLog.config.scenarios" file, or from this page, and copy it to your Nlog.config file.</li>
                    </ul>
                </li>                              
                <li>Run the application</li>
                <li>NLogSample.aspx should appear in your browser</li>
                <li>Click the button "Create Log Entry".  The logic behind the click event is as follows:
                    <ul>
                        <li>Logs a "Trace" level message "[method name] process started at: [date-time]"</li>
                        <li>Declares a "Person" object.</li>
                        <li>The constructor of the Person class, Logs an "info" level message "A 'Person' was instantiated"</li>
                        <li>It then logs an entry for every level, "I'm logging - [level]"</li>
                        <li>It then throws an exception which is caught in the try-catch block.</li>
                        <li>The "catch" statement logs the exception</li>
                        <li>The "finally" statement logs the following
                            <ul>
                                <li>Logs a "Trace" level message "[method name] process finished at: [date-time]"</li>
                                <li>Logs a "Info" level message "[method name] process duration: [elapse time] " ms"</li>
                                <li>If the time elapse is greater then 200ms, it logs a "Warning" level message "[method name] process exceeded the accepted elapse time, taking [elapse time] ms to execute"));</li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li>Check that the log entry was created in the specified target of your scenario. Log files will be created in the "logs" folder at the root of your project.</li>
                <li>Repeat the steps with another scenario from the "NLog.config.scenarios"</li>
            </ul>
        </div>
               
        <h2>Logging Scenarios</h2>
        <div>
            <h3>Scenario 1: Log all level messages, for all classes, to a file</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/NLogSample - ${machinename} - ${shortdate}.log"
         layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
&lt;/targets&gt;

&lt;rules&gt;
    &lt;logger name="*" minlevel="Trace" writeTo="logToFile" /&gt;
&lt;/rules&gt;
            </pre>
            <ul>
                <li>The result of this scenario will be a file with entries for all log levels, for all classes of the project ("NLogSample" and "Person" classes).</li>
                <li>This is achieved by setting a rule with:
                    <ul>
                        <li>the logger name to "*"</li>
                        <li>the "minLevel" to the lowest log level "Trace"</li>                           
                    </ul>
                </li>
                <li>and a Target specifying a file</li>
            </ul>
           
           
            <h3>Scenario 2: Log Error (and up) level messages, for all classes, to a file</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/NLogSample - ${machinename} - ${shortdate}.log"
         layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
&lt;/targets&gt;

&lt;rules&gt;
    &lt;logger name="*" minlevel="Error" writeTo="logToFile" /&gt;
&lt;/rules&gt;
            </pre>
            <ul>
                <li>The result of this scenario will be a file with entries for log levels "Error" and up, for all classes. You will see 2 entries for "NLogSample" class and no entry for the "Person" class as it only logs an "info" level message, which is below the "minLevel" specified in this scenario.</li>
                <li>This is achieved by setting a rule with:
                    <ul>
                        <li>the logger name to "*"</li>
                        <li>the "minLevel" attribute set to "Error"</li>
                    </ul>
                </li>
                <li>and a Target specifying a file</li>
                <li>A "maxLevel" attribute is also available should you want to limit the upper level messages.</li>

            </ul>
             
            <h3>Scenario 3: Log different level messages for different classes, to separate files</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/NLogSample - ${machinename} - ${shortdate}.log"
        layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;

    &lt;target xsi:type="File" name="logToPersonFile" fileName="${basedir}/logs/PersonClass - ${machinename} - ${shortdate}.log"
        layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
&lt;/targets&gt;

&lt;rules&gt;
    &lt;logger name="SampleCode.C3.NLogSample.NLogSample" minlevel="Warn" writeTo="logToFile" /&gt;&lt;
    &lt;logger name="SampleCode.C3.NLogSample.Person" minlevel="Trace" writeTo="logToPersonFile" /&gt;&lt;
/&lt;rules&gt;</pre>
            <ul>
                <li>The result of this scenario will be:
                    <ul>
                        <li>A file with entries for log levels "Warning", "Error" and "Fatal" for the "NLogSample" class.</li>
                        <li>A separate file with entries for All log levels for the "Person" class.</li>
                    </ul>
                </li>
                <li>This is achieve by setting multiple rules, with:
                    <ul>
                        <li>the logger name matching the name of the class(es)</li>
                        <li>the "minLevel" to the level required</li>
                        <li>the "writeTo" to the target required</li>
                    </ul>
                </li>
                <li>and by having multiple targets, in this case 2 targets, to have 1 file per class, "Person" and "NLogSample"</li>
            </ul>

            <h3>Scenario 4: Log all errors to a database stored procedure</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="Database"
          name="logToDB"
          keepConnection="false"
          useTransactions="true"
          dbProvider="Oracle.DataAccess.Client.OracleConnection, Oracle.DataAccess, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342"
          connectionString="Data Source=YYYYY.WORLD;User Id=AAAAA;Password=BBBBB;"
          commandText="LOG_PKG.INSERT_LOG"
          commandType="StoredProcedure"&gt;
      &lt;parameter name="pi_logLevel" layout="${level}" /&gt;
      &lt;parameter name="pi_logLogger" layout="${logger}" /&gt;
      &lt;parameter name="pi_Mlogessage" layout="${message}" /&gt;
      &lt;parameter name="pi_logDate" layout="${date:format=yyyy-MM-dd HH\:mm\:ss}" /&gt;
      &lt;parameter name="pi_MachineName" layout="${machinename}" /&gt;
    &lt;/target&gt;
&lt;/targets&gt;

&lt;rules&gt;
    &lt;logger name="*" minlevel="Trace" writeTo="logToDB" /&gt;
&lt;/rules&gt;
            </pre>
            <ul>
                <li>The result of this scenario will be a database record for each entry, for all log levels, for all classes of the project ("NLogSample" and "Person" classes).</li>
                <li>This is achieved by setting a rule with:
                    <ul>
                        <li>the logger name to "*"</li>
                        <li>the "minLevel" to the lowest log level "Trace"</li>                           
                    </ul>
                </li>
                <li>and a Target specifying the database and stored procedure details</li>
            </ul>

            <h3>Scenario 5: Log all errors to a database stored procedure with a fallback to file</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="FallbackGroup" name="logToDBWithFallBack" returnToFirstOnSuccess="true"&gt;
      &lt;target xsi:type="Database"
            name="logToDB"
            keepConnection="false"
            useTransactions="true"
            dbProvider="Oracle.DataAccess.Client.OracleConnection, Oracle.DataAccess, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342"
            connectionString="Data Source=YYYYY.WORLD;User Id=AAAAA;Password=BBBBB;"
            commandText="LOG_PKG.INSERT_LOG"
            commandType="StoredProcedure"&gt;
        &lt;parameter name="pi_logLevel" layout="${level}" /&gt;
        &lt;parameter name="pi_logLogger" layout="${logger}" /&gt;
        &lt;parameter name="pi_Mlogessage" layout="${message}" /&gt;
        &lt;parameter name="pi_logDate" layout="${date:format=yyyy-MM-dd HH\:mm\:ss}" /&gt;
        &lt;parameter name="pi_MachineName" layout="${machinename}" /&gt;
      &lt;/target&gt;

      &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/NLogSample - ${machinename} - ${shortdate}.log"
         layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
    &lt;/target&gt;
&lt;/targets&gt;

&lt;rules&gt;
    &lt;logger name="*" minlevel="Debug" writeTo="logToDBWithFallBack" /&gt;
&lt;/rules&gt;
            </pre>
            <ul>
                <li>The result of this scenario will the same as scenario 4, but if the database communication fails and the entry cannot be made to the database, it will be written to a file.</li>
                <li>This is achieved by setting:
                    <ul>
                        <li>a "Target" of type "FallbackGroup"</li>
                        <li>and including 2 targets, one for the database followed by the other for the file</li>
                    </ul>
                </li>
                <li>To test the fallback, simply invalidate the connection string by changing one its attributes</li>
            </ul>

            <h3>Scenario 6: Log messages to a service</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="WebService" name="logToService" namespace="http://tempuri.org/" protocol="Soap11" methodName="LogMe" url="http://localhost:4585/Samples/NLog.C3.Sample/LoggingServiceSample.svc"&gt;
        &lt;parameter name="message" type="System.String" layout="${message}"/&gt;
        &lt;parameter name="logger" type="System.String" layout="${logger}"/&gt;
        &lt;parameter name="level" type="System.String" layout="${level}"/&gt;
        &lt;parameter name="callerID" type="System.String" layout="TFW123"/&gt;
    &lt;/target&gt;

    &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/LoggingServiceSample - ${machinename} - ${shortdate}.log"
            layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
&lt;/targets&gt;
&lt;rules&gt;
    &lt;logger name="SampleCode.C3.NLogSample.NLogSample" minlevel="Debug" writeTo="logToService" /&gt;
    &lt;logger name="SampleCode.C3.NLogSample.Person" minlevel="Debug" writeTo="logToService" /&gt;
    &lt;logger name="SampleCode.C3.NLogSample.LoggingServiceSample" minlevel="Trace" writeTo="logToFile" /&gt;
&lt;/rules&gt;
            </pre>
             <ul>
                <li>The result of this scenario will be a service call for each log entry, for log levels "Trace" and up, for the NLogSample.aspx and Person class.</li>
                 <li>The service will receive the call and try to log it to a file.  The service also adds its own log entries to the file.</li>
                 <li>This is achieve by setting multiple rules, with:
                    <ul>
                        <li>the logger name to the name of the class</li>
                        <li>the "minLevel" to the lowest log level "Trace"</li>
                        <li>the "writeTo" to the target required</li>
                    </ul>
                </li>
                <li>and by having multiple targets, 1 for the web page and Person class to log to the service. The for service to log to a file.</li>
                <li>NOTE: Since the service and the sample page both write log messages and are in the same project, we cannot use "logger name='*'" as it will create an infinite loop.</li>
            </ul>

            <h3>Scenario 7: Log messages to a service with a fallback to file</h3>
            <pre class="prettyprint lang-xml">
&lt;targets&gt;
    &lt;target xsi:type="FallbackGroup" name="logToServiceWithFallBack" returnToFirstOnSuccess="true"&gt;
      &lt;target xsi:type="WebService" name="logToService" namespace="http://tempuri.org/" protocol="Soap11" methodName="LogMe" url="http://localhost:4585/Samples/NLog.C3.Sample/LoggingServiceSample.asmx"&gt;
        &lt;parameter name="message" type="System.String" layout="${message}"/&gt;
        &lt;parameter name="logger" type="System.String" layout="${logger}"/&gt;
        &lt;parameter name="level" type="System.String" layout="${level}"/&gt;
        &lt;parameter name="callerID" type="System.String" layout="TFW123"/&gt;
      &lt;/target&gt;

      &lt;target xsi:type="File" name="logToFallBackFile" fileName="${basedir}/logs/NLogSample - ${machinename} - ${shortdate}.log"
         layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;
    &lt;/target&gt;

    &lt;target xsi:type="File" name="logToFile" fileName="${basedir}/logs/LoggingServiceSample - ${machinename} - ${shortdate}.log"
         layout="${longdate}|${machinename}|${uppercase:${level}}|${uppercase:${logger}}|${threadid}|${message} ${exception:format=tostring}"/&gt;    
&lt;targets&gt;

&lt;rules&gt;
    &lt;logger name="SampleCode.C3.NLogSample.NLogSample" minlevel="Debug" writeTo="logToServiceWithFallBack" /&gt;
    &lt;logger name="SampleCode.C3.NLogSample.Person" minlevel="Debug" writeTo="logToService" /&gt;
    &lt;logger name="SampleCode.C3.NLogSample.LoggingServiceSample" minlevel="Trace" writeTo="logToFile" /&gt;
&lt;/rules&gt;
            </pre>
            <ul>
                <li>The result of this scenario will the same as scenario 6, but if the service communication fails and the entry cannot be made to the service, it will be written to a file.</li>
                <li>This is achieved by setting:
                    <ul>
                        <li>a "Target" of type "FallbackGroup"</li>
                        <li>and including 2 targets, one for the service followed by the other for the file</li>
                        <li>To test the fallback, simply invalidate the url to the service.</li>
                    </ul>
                </li>
            </ul>
        </div>

        <h2>Steps to open in excel</h2>
       
    </form>
</body>
</html>
