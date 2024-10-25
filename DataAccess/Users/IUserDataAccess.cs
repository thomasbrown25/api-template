using template_api.Dtos.User;

namespace template_api.DataAccess.Users
{
    public interface IUserDataAccess
    {
        Task<bool> UserExists(string email);
        Task<LoadUserDto?> AddUser(AddUserDto user);
        Task<LoadUserDto?> UpdateUser(LoadUserDto user);
        Task<LoadUserDto> GetCurrentUser();
        Task<LoadUserDto> GetUser(string email);
        Task<User> GetUser(int id);
        Task<AddUserDto> GetRegisteredUser(string email);
        Task<LoadUserDto> ValidateUser(AddUserDto user, string password);
        void DeleteUser(User user);
        void SaveContextAsync();
    }
}