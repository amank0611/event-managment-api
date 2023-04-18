using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public RepositoryResponse CreateUser(UserDto userModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Users>> GetAllUser()
        {
            try
            {
                return await _applicationDbContext.Users.Where(p => p.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IList<Roles>> GetRoles()
        {
            return await _applicationDbContext.Roles.ToListAsync();
        }

        public IList<Users> GetUserByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
