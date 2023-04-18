using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<RepositoryResponse> CreateUser(UserDto userModel);
        Task<IList<UserDto>> GetAllUser();
        Task<IList<UserDto>> GetUserByUserId(int userId);
        Task<IList<RolesDto>> GetRoles();
    }
}
