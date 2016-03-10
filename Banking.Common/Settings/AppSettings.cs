using System.Configuration;
using System.Web.Configuration;
using Banking.Common.Identifiers;

namespace Banking.Common.Settings
{
    public class AppSettings
    {
        private static readonly string _connectionString ;
        private static readonly string _clientId;
        private static readonly string _clientSecret;
        private static readonly string _domain;
        private static readonly string _issuer;

        static AppSettings()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[Constatnts.ConnectionStringName].ConnectionString;
            _clientId = WebConfigurationManager.AppSettings["auth0:ClientId"];
            _clientSecret = WebConfigurationManager.AppSettings["auth0:ClientSecret"];
            _domain = WebConfigurationManager.AppSettings["auth0:Domain"];
            _issuer = WebConfigurationManager.AppSettings["auth0:issuer"];
        
        }
        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        public static string ClientId
        {
            get { return _clientId; }
        }

        public static string ClientSecret
        {
            get { return _clientSecret; }
        }

        public static string Domain
        {
            get { return _domain; }
        }

        public static string Issuer
        {
            get { return _issuer; }
        }
    }
}
