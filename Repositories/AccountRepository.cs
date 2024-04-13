using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Iduff.Repositories
{
   public class AccountRepository(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IAccountRepository
    {
    
        public async Task<ServiceResponses.GeneralResponse> CreateAccount(UserDto userDto)
        {
            if (userDto is null) return new ServiceResponses.GeneralResponse(false, "Model is empty");
            var newUser = new Usuario()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                UserName = userDto.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new ServiceResponses.GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
            if (!createUser.Succeeded) return new ServiceResponses.GeneralResponse(false, createUser.Errors.FirstOrDefault()?.Description ?? "Um erro desconhecido aconteceu, contate o admin.");

            string role = userDto.Role == UserRole.Admin ? "Admin" : "User";

            await userManager.AddToRoleAsync(newUser, role);
            return new ServiceResponses.GeneralResponse(true, "Account Created");
        }

        public async Task<ServiceResponses.LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            if (loginDTO == null)
                return new ServiceResponses.LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new ServiceResponses.LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new ServiceResponses.LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new ServiceResponses.LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}