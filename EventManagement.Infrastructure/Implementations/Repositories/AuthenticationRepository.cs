using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Implementations.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public AuthenticationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Users> GetUserRole(int userId)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<bool> RegisterUser(Users users, int roleId)
        {
            bool updatedResult = false;
            int newUserId = 0;
            users.IsActive = true;
            users.CreatedDate = DateTime.Now;
            _applicationDbContext.Users.Add(users);
            await _applicationDbContext.SaveChangesAsync();
            newUserId = users.UserId;
            if (newUserId > 0)
            {
                updatedResult = true;
            }
            return updatedResult;
        }

        public async Task<Users> UserLogin(string email, string password)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password && p.IsActive == true);
        }
    }
}
