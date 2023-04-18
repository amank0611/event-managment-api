using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<Users> UserLogin(string email, string password);
        Task<bool> RegisterUser(Users loginDto, int roleId);
        Task<Users> GetUserRole(int userId);
    }
}
