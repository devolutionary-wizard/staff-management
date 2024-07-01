using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
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
            try
            {
                const string query = "SELECT staff_id AS StaffId, full_name AS FullName, birthday, gender FROM Staff;";
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<Staff>(query);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all staff members.");
                return StatusCode(500, "Unable to process request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStaff(Staff staff)
        {
            var newStaff = new Staff();
            try
            {


                string query = @"INSERT INTO staff (staff_id, full_name, birthday, gender) 
                 VALUES (@StaffId, @FullName, @Birthday, @Gender)";
                var param = new DynamicParameters();
                param.Add("@StaffId", staff.StaffId);
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
                return Ok(newStaff);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        [HttpPut("{staffId}")]
        public async Task<IActionResult> UpdateStaff(string staffId, Staff staff)
        {
            if (staffId != staff.StaffId)
            {
                return BadRequest("Staff ID mismatch");
            }

            try
            {
                string query = @"UPDATE staff SET full_name = @FullName, birthday = @Birthday, gender = @Gender 
                         WHERE staff_id = @StaffID";
                var param = new DynamicParameters();
                param.Add("@StaffID", staffId);
                param.Add("@FullName", staff.FullName);
                param.Add("@Birthday", staff.Birthday);
                param.Add("@Gender", staff.Gender);

                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        return Ok(staff);
                    }
                    else
                    {
                        return NotFound($"Staff with ID {staffId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{staffId}")]
        public async Task<IActionResult> DeleteStaff(string staffId)
        {
            try
            {
                string query = @"DELETE FROM staff WHERE staff_id = @StaffID";
                var param = new DynamicParameters();
                param.Add("@StaffID", staffId);

                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        return Ok($"Staff with ID {staffId} has been deleted.");
                    }
                    else
                    {
                        return NotFound($"Staff with ID {staffId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}