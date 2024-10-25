using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using template_api.DataAccess.Users;
using template_api.Services.LoggingService;
using template_api.Dtos.User;
using Microsoft.IdentityModel.Tokens;
using template_api.Enums;

namespace template_api.Services.UserService
{
    public class UserService(
        IUserDataAccess userDataAccess,
        IConfiguration configuration,
        ILoggingService loggingService) : IUserService
    {
        private readonly IUserDataAccess _userDataAccess = userDataAccess;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILoggingService _loggingService = loggingService;

        public async Task<ServiceResponse<LoadUserDto>> Register(AddUserDto user)
        {
            ServiceResponse<LoadUserDto> response = new();

            try
            {
                if (await _userDataAccess.UserExists(user.Email))
                {
                    response.Message = "A user with that email already exists.";
                    response.Success = false;
                    _loggingService.LogTrace("Register user failed: A user with that email already exists.");
                    return response;
                }

                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                var token = CreateToken(user);

                var loadedUser = await _userDataAccess.AddUser(user);

                response.Data = new LoadUserDto();
                response.Data = loadedUser;
                response.Data.JWTToken = token;

            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<LoadUserDto>> Login(string email, string password)
        {
            ServiceResponse<LoadUserDto> response = new();

            try
            {
                var user = await _userDataAccess.GetRegisteredUser(email);

                var validUser = await _userDataAccess.ValidateUser(user, password);

                if (validUser is null)
                {
                    response.Success = false;
                    response.Message = "Invalid email or password";
                    return response;
                }

                validUser.JWTToken = CreateToken(user);

                response.Data = validUser;
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                response.Success = false;
                response.Message = "Sorry we ran into an issue. Please contact support for assistance.";
            }
            return response;
        }

        public async Task<ServiceResponse<LoadUserDto>> LoadUser()
        {
            ServiceResponse<LoadUserDto> response = new();

            try
            {
                var user = await _userDataAccess.GetCurrentUser();

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    return response;
                }

                response.Data = user;
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUser(int userId)
        {
            ServiceResponse<string> response = new();

            try
            {
                User user = await _userDataAccess.GetUser(userId);

                _userDataAccess.DeleteUser(user);
                _userDataAccess.SaveContextAsync();

                response.Data = "User Deleted: " + user.FirstName;
            }
            catch (Exception ex)
            {
                _loggingService.LogException(ex);
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


        private void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(AddUserDto user)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, user.Email),
                new(ClaimTypes.Name, user.Email)
            ];

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration["Key"]
                )
            );

            SigningCredentials creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    double.Parse(_configuration["JWTTokenExpiration"])
                ),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
