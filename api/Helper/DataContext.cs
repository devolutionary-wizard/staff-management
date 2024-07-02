using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Api.Helper
{
    public class DatabaseContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connString;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var host = _configuration["DB_HOST"] ?? "localhost";
            var port = _configuration["DBPORT"] ?? "3306";
            var password = _configuration["DB_PASSWORD"] ?? _configuration.GetConnectionString("DB_PASSWORD");
            var userid = _configuration["DB_USER"] ?? _configuration.GetConnectionString("DB_USER");
            var usersDataBase = _configuration["DB_NAME"] ?? _configuration.GetConnectionString("DB_NAME");

            _connString = $"server={host}; userid={userid}; password={password}; port={port}; database={usersDataBase}";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connString);
        }
    }
}