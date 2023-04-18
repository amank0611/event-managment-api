using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Application.Interfaces.Services;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _autoMapper;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IMapper autoMapper, IAuthenticationRepository authenticationRepository)
        {
            _autoMapper = autoMapper;
            _authenticationRepository = authenticationRepository;
        }

        public Task<bool> RegisterUser(UserDto loginDto)
        {
            var roleId = loginDto.RoleId;
            var user = _autoMapper.Map<Users>(loginDto);
            return _authenticationRepository.RegisterUser(user, roleId);
        }

        public async Task<RepositoryResponse> UserLogin(string email, string password)
        {
            var user = await _authenticationRepository.UserLogin(email, password);
            if (user != null)
            {
                return new RepositoryResponse(new
                {
                    Response = user
                });
            }
            else
            {
                return new RepositoryResponse(new
                {
                    Response = "User not found, Please register."
                });
            }
        }
    }
}
