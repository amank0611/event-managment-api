using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<RepositoryResponse> UserLogin(string email, string password);
        Task<bool> RegisterUser(UserDto loginDto);
    }
}
