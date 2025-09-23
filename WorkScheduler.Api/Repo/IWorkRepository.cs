namespace WorkScheduler.Api.Repo
{
    public interface IWorkRepository
    {

        // Fetching
        Task<List<UserDisplayDTO>> GetAllUsersAsync();
        Task<UserDisplayDTO> GetUserByIdAsync(int userId);
        Task<List<ShiftDisplayDTO>> GetAllShiftsAsync();
        Task<List<ShiftDisplayDTO>> GetAllShiftsByUserAsync(int userId);

        // Creating
        Task<UserDisplayDTO> AddUserAsync(UserCreateDTO user);
        Task<ShiftDisplayDTO> AddShiftAsync(ShiftCreateDTO shift);

        // Update
        Task<ShiftDisplayDTO> UpdateShiftAsync(ShiftUpdateDTO shift, int shiftId);
        Task<UserDisplayDTO> UpdateUserAsync(UserUpdateDTO user, int userId);

        // Delete
        Task<bool> DeleteUserByIdAsync(int userId);
        Task<bool> DeleteUserByNameAsync(string username);
        Task<bool> DeleteShiftByIdAsync(int shiftId);

    }
}
