using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;

namespace IdentityManagerServerApi.Repositories
{
   public class AccountRepository(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
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
            if (!createUser.Succeeded) return new ServiceResponses.GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new ServiceResponses.GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new ServiceResponses.GeneralResponse(true, "Account Created");
            }
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