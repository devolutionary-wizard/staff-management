using dotenv.net;
using MySql.Data.MySqlClient;

namespace Api.Helper
{
    public class DatabaseContext
    {
        private readonly string _connString;
        public DatabaseContext()
        {
            DotEnv.Load();
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DBPORT") ?? "3306";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var userid = Environment.GetEnvironmentVariable("DB_USER");
            var usersDataBase = Environment.GetEnvironmentVariable("DB_NAME");

            _connString = $"server={host}; userid={userid}; password={password}; port={port}; database={usersDataBase}";
        }


        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connString);
        }
    }
}