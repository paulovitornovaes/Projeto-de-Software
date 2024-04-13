using Iduff.Dtos;

namespace Iduff.Contracts;

public interface IAccountRepository
{
    Task<ServiceResponses.GeneralResponse> CreateAccount(UserDto userDTO);
    Task<ServiceResponses.LoginResponse> LoginAccount(LoginDto loginDTO);
}