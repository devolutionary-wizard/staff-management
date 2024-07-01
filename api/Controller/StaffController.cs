using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Api.Model;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]

    public class StaffController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string connString;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IConfiguration configuration, ILogger<StaffController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var host = _configuration["DB_HOST"] ?? "localhost";
            var port = _configuration["DBPORT"] ?? "3306";
            var password = _configuration["DB_PASSWORD"] ?? _configuration.GetConnectionString("DB_PASSWORD");
            var userid = _configuration["DB_USER"] ?? _configuration.GetConnectionString("DB_USER");
            var usersDataBase = _configuration["DB_NAME"] ?? _configuration.GetConnectionString("DB_NAME");

            connString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";
        }
        [HttpGet]
        public async Task<ActionResult<List<Staff>>> GetAllStaffs()
        {
            var staff = new List<Staff>();
            try
            {
                string query = @"SELECT * FROM staff";
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<Staff>(query, CommandType.Text);
                    staff = result.ToList();
                }
                return Ok(staff);
            }
            catch (Exception)
            {
                return StatusCode(500, "Unable To Process Request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStaff(Staff staff)
        {
            var newStaff = new Staff();
            try
            {
                string query = @"INSERT INTO staff (staff_id, full_name, birthday, gender) 
                 VALUES (@StaffID, @FullName, @Birthday, @Gender)";
                var param = new DynamicParameters();
                param.Add("@StaffID", Guid.NewGuid().ToString().Substring(0, 8));
                param.Add("@FullName", staff.FullName);
                param.Add("@Birthday", staff.Birthday);
                param.Add("@Gender", staff.Gender);
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        newStaff = staff;
                    }
                }

                if (newStaff != null)
                {
                    return Ok(newStaff);
                }
                else
                {
                    return BadRequest("Unable To  User");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

    }
}