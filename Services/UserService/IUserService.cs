
using template_api.Dtos.User;

namespace template_api.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<LoadUserDto>> Register(AddUserDto user);
        Task<ServiceResponse<LoadUserDto>> Login(string email, string password);
        Task<ServiceResponse<LoadUserDto>> LoadUser();
        Task<ServiceResponse<string>> DeleteUser(int userId);
    }
}