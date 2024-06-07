using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Humanizer;
using Iduff.Contracts;
using Iduff.Dtos;
using Iduff.Models;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Iduff.Repositories
{
   public class AccountRepository(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IAccountRepository
   {
        private IAlunoRepository _alunoRepository;
        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        public AccountRepository(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IAlunoRepository alunoRepository) : this(userManager, roleManager, config)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<ServiceResponses.GeneralResponse> CreateAccount(UserDto userDto)
        {
            if (userDto is null) return new ServiceResponses.GeneralResponse(false, "Model is empty");
            if (!string.IsNullOrEmpty(userDto.Matricula))
            {
                var existingUserByMatricula = await _alunoRepository.GetAlunoByMatricula(userDto.Matricula);
                
                if (existingUserByMatricula is not null) return new ServiceResponses.GeneralResponse(false, "Matrícula já existe");
                
                var newUser = new Aluno()
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    UserName = userDto.Email,
                    matricula = long.Parse(userDto.Matricula),
                    UserType = "Aluno"
                };
               
                var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
                if (!createUser.Succeeded) return new ServiceResponses.GeneralResponse(false, createUser.Errors.FirstOrDefault()?.Description ?? "Um erro desconhecido aconteceu, contate o admin.");
                string role = "User";

                await userManager.AddToRoleAsync(newUser, role);
                return new ServiceResponses.GeneralResponse(true, "Account Created");
            }
            else
            {

                var newUser = new Usuario()
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    UserName = userDto.Email,
                    UserType = "Administrador"
                };
                
                var user = await userManager.FindByEmailAsync(newUser.Email);
                if (user is not null) return new ServiceResponses.GeneralResponse(false, "User registered already");

                var createUser = await userManager.CreateAsync(newUser!, userDto.Password);
                if (!createUser.Succeeded) return new ServiceResponses.GeneralResponse(false, createUser.Errors.FirstOrDefault()?.Description ?? "Um erro desconhecido aconteceu, contate o admin.");
                
                string role = "Admin";

                await userManager.AddToRoleAsync(newUser, role);
                return new ServiceResponses.GeneralResponse(true, "Account Created");
                
            }
                

        }

        public async Task<ServiceResponses.LoginResponse> LoginAccount(LoginDto loginDTO)
        {
            if (loginDTO == null)
                return new ServiceResponses.LoginResponse(false, null!, "Login container is empty", null);

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new ServiceResponses.LoginResponse(false, null!, "User not found", null);

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new ServiceResponses.LoginResponse(false, null!, "Invalid email/password", null);

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            var token = GenerateToken(userSession);
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            var teste = GetExpiryTimestamp(tokenHandler);
            var matricula = await _alunoRepository.GetMatriculaByEmail(getUser.Email);
            return new ServiceResponses.LoginResponse(true, tokenHandler, "Login completed", matricula);
        }

        private JwtSecurityToken GenerateToken(UserSession user)
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return token;
        }
        
        public DateTime GetExpiryTimestamp(string accessToken)
        {
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
                return dateTimeOffset.LocalDateTime;
            }
            catch (TokenExpiredException)
            {
                return DateTime.MinValue;
            }
            catch (SignatureVerificationException)
            {
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                // ... remember to handle the generic exception ...
                return DateTime.MinValue;
            }
        }
    }
}