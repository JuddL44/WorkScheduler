using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WorkScheduler.Api.Repo;

namespace WorkScheduler.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkController : ControllerBase
    {

        private readonly IWorkRepository repo;
        public WorkController(IWorkRepository _repo) => repo = _repo;



        // Fetching

        [HttpGet("users/get")]
        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            List<UserDisplayDTO> users = await repo.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/get/{id}")]
        public async Task<ActionResult<User>> GetByIdAsync(int id)
        {
            var user = await repo.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("shifts/get")]
        public async Task<ActionResult<List<Shift>>> GetAllShiftsAsync()
        {
            var shifts = await repo.GetAllShiftsAsync();
            return Ok(shifts);
        }

        [HttpGet("shifts/get/user/{userId}")]
        public async Task<ActionResult<List<Shift>>> GetAllShiftsByUserIdAsync(int userId)
        {
            var shifts = await repo.GetAllShiftsByUserAsync(userId);
            return Ok(shifts);
        }


        // Creating

        [HttpPost("users/create")]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserCreateDTO userDto)
        {
            var user = await repo.AddUserAsync(userDto);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }

        [HttpPost("shifts/create")]
        public async Task<ActionResult> CreateShiftAsync([FromBody] ShiftCreateDTO shiftDto)
        {
            var shift = await repo.AddShiftAsync(shiftDto);
            if (shift == null) { return NotFound(); }
            return Ok(shift);
        }


        // Update

        [HttpPut("shifts/update/{shiftId}")]
        public async Task<ActionResult<ShiftDisplayDTO>> UpdateShiftAsync(int shiftId, [FromBody] ShiftUpdateDTO shiftDto)
        {
            var shift = await repo.UpdateShiftAsync(shiftDto, shiftId);
            if (shift == null) { return NotFound(); }
            return Ok(shift);
        }

        [HttpPut("users/update/{userId}")]
        public async Task<ActionResult<UserDisplayDTO>> UpdateUserAsync(int userId, [FromBody] UserUpdateDTO userDto)
        {
            var user = await repo.UpdateUserAsync(userDto, userId);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }


        // Delete

        [HttpDelete("users/delete/id/{userId}")]
        public async Task<ActionResult> DeleteUserByIdAsync(int userId)
        {
            var valid = await repo.DeleteUserByIdAsync(userId);
            if (valid)
            {
                return Ok("Deleted user successfully.");
            }
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("users/delete/username/{username}")]
        public async Task<ActionResult> DeleteUserByNameAsync(string username)
        {
            var valid = await repo.DeleteUserByNameAsync(username);
            if (valid)
            {
                return Ok("Deleted user successfully.");
            }
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("shifts/delete/id/{shiftId}")]
        public async Task<ActionResult> DeleteShiftByIdAsync(int shiftId)
        {
            var valid = await repo.DeleteShiftByIdAsync(shiftId);
            if (valid)
            {
                return Ok("Deleted shift successfully.");
            }
            return BadRequest("Something went wrong.");
        }










    }
}
