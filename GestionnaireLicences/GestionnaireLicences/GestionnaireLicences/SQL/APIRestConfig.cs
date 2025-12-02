using System;
using System.Collections.Generic;
using System.Text;

namespace GestionnaireLicences.SQL
{
    public class ApiRestConnectionString
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string BuildConnectionString()
        {
            // You can set environment variable name which stores your real value,
            // or use as value if not configured as environment variable
            string server = Environment.GetEnvironmentVariable(Server) ?? Server;
            string port = Environment.GetEnvironmentVariable(Port) ?? Port;
            string database = Environment.GetEnvironmentVariable(Database) ?? Database;
            string user = Environment.GetEnvironmentVariable(User) ?? User;
            string password = Environment.GetEnvironmentVariable(Password) ?? Password;

            string connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password}";

            return connectionString;
        }
    }
}

