using Microsoft.AspNetCore.Mvc;
using Dapper;
using Api.Model;
using Api.Utils;
using System.Text;
using Api.Helper;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<StaffController> _logger;
        private readonly DatabaseContext _databaseContext;

        public StaffController(ILogger<StaffController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Staff>>> GetAllStaffs(string? staffId = null, string? gender = null, DateTime? birthdayFrom = null, DateTime? birthdayTo = null)
        {
            try
            {
                var queryBuilder = new StringBuilder("SELECT staff_id AS StaffId, full_name AS FullName, birthday, gender FROM staff WHERE 1=1");

                if (!string.IsNullOrEmpty(staffId))
                {
                    queryBuilder.Append(" AND staff_id = @StaffId");
                }
                if (!string.IsNullOrEmpty(gender))
                {
                    queryBuilder.Append(" AND gender = @Gender");
                }
                if (birthdayFrom.HasValue)
                {
                    queryBuilder.Append(" AND birthday >= @BirthdayFrom");
                }
                if (birthdayTo.HasValue)
                {
                    queryBuilder.Append(" AND birthday <= @BirthdayTo");
                }
                using (var connection = _databaseContext.GetConnection())
                {
                    var result = await connection.QueryAsync<Staff>(queryBuilder.ToString(), new { StaffId = staffId, Gender = gender, BirthdayFrom = birthdayFrom, BirthdayTo = birthdayTo });
                    ApiResponse<List<Staff>> response = new ApiResponse<List<Staff>>(200, "Success", result.ToList());
                    return Ok(response);
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
            try
            {

                var today = DateTime.Today;
                var age = today.Year - staff.Birthday.Year;
                if (staff.Birthday.Date > today.AddYears(-age)) age--;
                if (age < 16) return BadRequest("Staff must be at least 16 years old.");

                string query = @"INSERT INTO staff (staff_id, full_name, birthday, gender) 
                                 VALUES (@StaffId, @FullName, @Birthday, @Gender)";
                var param = new DynamicParameters();
                param.Add("@StaffId", staff.StaffId);
                param.Add("@FullName", staff.FullName);
                param.Add("@Birthday", staff.Birthday);
                param.Add("@Gender", staff.Gender);

                using (var connection = _databaseContext.GetConnection())
                {
                    var result = await connection.ExecuteAsync(query, param);
                    if (result > 0)
                    {
                        return Ok(staff);
                    }
                    else
                    {
                        return BadRequest("Failed to add new staff.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new staff.");
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
                                 WHERE staff_id = @StaffId";
                var param = new DynamicParameters();
                param.Add("@StaffId", staffId);
                param.Add("@FullName", staff.FullName);
                param.Add("@Birthday", staff.Birthday);
                param.Add("@Gender", staff.Gender);

                using (var connection = _databaseContext.GetConnection())
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
                _logger.LogError(ex, "Error updating staff.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{staffId}")]
        public async Task<IActionResult> DeleteStaff(string staffId)
        {
            try
            {
                string query = @"DELETE FROM staff WHERE staff_id = @StaffId";
                var param = new DynamicParameters();
                param.Add("@StaffId", staffId);

                using (var connection = _databaseContext.GetConnection())
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
                _logger.LogError(ex, "Error deleting staff.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}