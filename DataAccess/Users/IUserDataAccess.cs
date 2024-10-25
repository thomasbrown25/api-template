using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserSetting;

namespace personal_trainer_api.DataAccess.Users
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
        Task<SettingsDto> AddUserSettings(int userId);
        Task<SettingsDto> UpdateUserSettings(SettingsDto settingsDto);
        Task<SettingsDto> GetUserSettings(int userId);
    }
}