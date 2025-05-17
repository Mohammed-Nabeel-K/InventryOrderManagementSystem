using AutoMapper;
using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.Helpers.JWT;
using InventryOrderManagementSystem.BLL.Security;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InventryOrderManagementSystem.BLL.Services
{
    public class AuthServices(
        IAuthRepository authRepository,
        IJwtHelper jwtHelper,
        IPasswordHasher passwordHasher,
        ILogger<AuthServices> logger,
        IMapper mapper) : IAuthServices
    {
        public async Task<Response<object>> LoginAsync(LoginDto loginDto)
        {
            // Validate the user credentials
            var user = await authRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || !passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHashed))
            {
                logger.LogError("Login attept failed invalid username or password for {email}",loginDto.Email);
                return new Response<object>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Invalid email or password"
                };
            }
            // Generate JWT token
            var token = jwtHelper.GenerateToken(user);
            logger.LogInformation("User {email} logged in successfully", loginDto.Email);
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
                logger.LogError("User registration failed, user already exists with email {email}", userDto.Email);
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
                logger.LogError("User registration failed for {email}", userDto.Email);
                return new Response<User>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Failed to create user"
                };
            }
            logger.LogInformation("User {email} registered successfully", userDto.Email);
            return new Response<User>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "User created successfully"
            };
        }
    }
}
