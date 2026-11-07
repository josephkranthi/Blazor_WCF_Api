using System;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WcfService1
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialize the Enterprise Library Database provider factory from configuration
            var configSource = ConfigurationSourceFactory.Create();
            var dbProviderFactory = new DatabaseProviderFactory(configSource);

            // Set the static DatabaseFactory so DatabaseFactory.CreateDatabase(name) works
            DatabaseFactory.SetDatabaseProviderFactory(dbProviderFactory);
        }
    }
}
