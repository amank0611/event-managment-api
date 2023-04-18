using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using EventManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        RepositoryResponse CreateUser(UserDto userModel);
        Task<IList<Users>> GetAllUser();
        IList<Users> GetUserByUserId(int userId);
        Task<IList<Roles>> GetRoles();
    }
}
