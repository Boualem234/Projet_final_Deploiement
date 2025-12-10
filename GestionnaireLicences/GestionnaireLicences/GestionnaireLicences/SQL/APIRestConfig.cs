using System;

namespace GestionnaireLicences.SQL
{
    public class ApiRestConnectionString
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string BuildConnectionString()
        {
            // Si tu veux utiliser directement les valeurs de appsettings.json
            string server = Server; 
            string database = Database;   
            string user = User;         
            string password = Password; 

            string connectionString =
                $"Server={server};Database={database};User ID={user};Password={password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            return connectionString;
        }
    }
}
