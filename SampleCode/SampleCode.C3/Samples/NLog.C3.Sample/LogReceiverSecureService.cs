using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace SampleCode.C3.Samples
{
    public class LogReceiverSecureService : NLog.Targets.LogReceiverWebServiceTarget
    {
        /// <summary>
        /// Gets or sets the UserName of the service when it's authentication is set to UserName
        /// </summary>
        /// <value>The name of the endpoint configuration.</value>
        public string ServiceUsername { get; set; }

        /// <summary>
        /// Gets or sets de Password of the service when it's authentication is set to UserName
        /// </summary>
        public string ServicePassword { get; set; }

        /// <summary>
        /// Creates a new instance of WcfLogReceiverClient.
        /// We make override over this method to allow the authentication
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        protected override NLog.LogReceiverService.WcfLogReceiverClient CreateWcfLogReceiverClient()
        {
            //var client = base.CreateWcfLogReceiverClient();

            NLog.LogReceiverService.WcfLogReceiverClient client = new NLog.LogReceiverService.WcfLogReceiverClient(true);
            
            if (client.ClientCredentials != null)
            {
                // You could use the config file configuration (this example) or you could hard-code it (if you do not want to expose the credentials)
                client.ClientCredentials.UserName.UserName = this.ServiceUsername;
                client.ClientCredentials.UserName.Password = this.ServicePassword;
            }
            return client;
        }
    }
}