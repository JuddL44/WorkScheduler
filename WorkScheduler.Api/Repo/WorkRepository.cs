
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkScheduler.Api.Data;

namespace WorkScheduler.Api.Repo
{
    public class WorkRepository : IWorkRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        public WorkRepository(AppDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }


        // Fetching
        public async Task<List<ShiftDisplayDTO>> GetAllShiftsAsync()
        {
            List<Shift> shifts = await context.Shifts.ToListAsync();
            return mapper.Map<List<ShiftDisplayDTO>>(shifts);
        }
        public async Task<List<ShiftDisplayDTO>> GetAllShiftsByUserAsync(int userId)
        {
            var shifts = await context.Shifts.Where(s => s.AssignedUserId == userId).ToListAsync();
            return mapper.Map<List<ShiftDisplayDTO>>(shifts);
        }
        public async Task<List<UserDisplayDTO>> GetAllUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            return mapper.Map<List<UserDisplayDTO>>(users);
        }
        public async Task<UserDisplayDTO> GetUserByIdAsync(int userId)
        {
            var user = await context.Users.FindAsync(userId);
            return mapper.Map<UserDisplayDTO>(user);
        }

        // Creating
        public async Task<ShiftDisplayDTO> AddShiftAsync(ShiftCreateDTO shiftDto)
        {
            var shift = mapper.Map<Shift>(shiftDto);
            await context.Shifts.AddAsync(shift);
            await context.SaveChangesAsync();
            return mapper.Map<ShiftDisplayDTO>(shift);
        }
        public async Task<UserDisplayDTO> AddUserAsync(UserCreateDTO userDto)
        {
            var user = mapper.Map<User>(userDto);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return mapper.Map<UserDisplayDTO>(user);
        }

        // Update
        public async Task<ShiftDisplayDTO> UpdateShiftAsync(ShiftUpdateDTO shiftDto, int shiftId)
        {
            var shift = await context.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                mapper.Map(shiftDto, shift);
                await context.SaveChangesAsync();
            }
            return mapper.Map<ShiftDisplayDTO>(shift);
        }
        public async Task<UserDisplayDTO> UpdateUserAsync(UserUpdateDTO userDto, int userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                mapper.Map(userDto, user);
                await context.SaveChangesAsync();
                return mapper.Map<UserDisplayDTO>(user);
            }
            return mapper.Map<UserDisplayDTO>(user);
        }


        // Delete
        public async Task<bool> DeleteShiftByIdAsync(int shiftId)
        {
            var shift = await context.Shifts.FirstOrDefaultAsync(s => s.Id == shiftId);
            if (shift != null)
            {
                context.Shifts.Remove(shift);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserByIdAsync(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUserByNameAsync(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
