using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models.Post;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LibroMind_BE.Common.DateTimeProvider;
using System.Security.Cryptography;
using LibroMind_BE.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LibroMind_BE.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AuthenticationService(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<string> LoginAsync(LoginPostDTO loginPost)
        {
            User? existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(loginPost.Email);

            if (existingUser is null || !VerifyCredentials(loginPost.Password, existingUser.Password, existingUser.Salt))
            {
                throw new BadHttpRequestException("Invalid credentials", StatusCodes.Status401Unauthorized);
            }

            string token = GenerateToken(existingUser);

            return JsonSerializer.Serialize(new { token });
        }

        public async Task RegisterAsync(RegisterPostDTO registerPost)
        {
            CreateHash(registerPost.Password, out byte[] hash, out byte[] salt);

            User? newUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(registerPost.Email);

            if (newUser is not null)
            {
                throw new BadHttpRequestException(
                    "This email address was already used",
                    StatusCodes.Status401Unauthorized);
            }

            newUser = new User()
            {
                AddressId = registerPost.AddressId,
                RoleId = 1,
                FirstName = registerPost.FirstName,
                LastName = registerPost.LastName,
                BirthDate = registerPost.BirthDate,
                Email = registerPost.Email,
                Phone = registerPost.Phone,
                Password = hash,
                Salt = salt
            };

            _unitOfWork.UserRepository.Add(newUser);

            await _unitOfWork.CommitAsync();
        }

        private string GenerateToken(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes("my-top-secret-key"));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: _dateTimeProvider.UtcNow.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void CreateHash(string password, out byte[] hash, out byte[] salt)
        {
            using HMACSHA512 hmac = new();

            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyCredentials(string password, byte[] hash, byte[] salt)
        {
            using HMACSHA512 hmac = new(salt);

            byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return hash.SequenceEqual(computedHash);
        }
    }
}
