using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.Helpers.JWT;
using InventryOrderManagementSystem.BLL.Security;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using System.Net;

namespace InventryOrderManagementSystem.BLL.Services
{
    public class AuthServices(
        IAuthRepository authRepository,
        IJwtHelper jwtHelper,
        IPasswordHasher passwordHasher,
        IMapper mapper) : IAuthServices
    {
        public async Task<Response<object>> LoginAsync(LoginDto loginDto)
        {
            // Validate the user credentials
            var user = await authRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHashed))
            {
                return new Response<object>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Invalid email or password"
                };
            }
            // Generate JWT token
            var token = jwtHelper.GenerateToken(user);
            return new Response<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Login successful",
                Data = new { Token = token }
            };
        }

        public async Task<Response<User>> RegisterAsync(UserDto userDto)
        {
            // Check if the user already exists
            var existingUser = await authRepository.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return new Response<User>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Message = "User already exists"
                };
            }
            // Create a new user
            var newUser = mapper.Map<User>(userDto);
            newUser.PasswordHashed = passwordHasher.HashPassword(userDto.Password);
            var result = await authRepository.CreateUserAsync(newUser);
            if (!result)
            {
                return new Response<User>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Failed to create user"
                };
            }
            return new Response<User>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "User created successfully"
            };
        }
    }
}
