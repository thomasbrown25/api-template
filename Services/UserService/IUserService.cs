
using personal_trainer_api.Dtos.User;
using personal_trainer_api.Dtos.UserSetting;

namespace personal_trainer_api.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<LoadUserDto>> Register(AddUserDto user);
        Task<ServiceResponse<LoadUserDto>> Login(string email, string password);
        Task<ServiceResponse<LoadUserDto>> LoadUser();
        Task<ServiceResponse<string>> DeleteUser(int userId);
        Task<ServiceResponse<SettingsDto>> GetSettings();
        Task<ServiceResponse<SettingsDto>> SaveSettings(SettingsDto newSettings);
    }
}