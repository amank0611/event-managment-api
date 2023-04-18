using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper autoMapper, IUserRepository userRepository)
        {
            _autoMapper = autoMapper;
            _userRepository = userRepository;
        }

        public Task<RepositoryResponse> CreateUser(UserDto userModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UserDto>> GetAllUser()
        {
            var data = await _userRepository.GetAllUser();
            return _autoMapper.Map<List<UserDto>>(data);
        }

        public async Task<IList<RolesDto>> GetRoles()
        {
            var data = await _userRepository.GetRoles();
            return _autoMapper.Map<List<RolesDto>>(data);
        }

        public Task<IList<UserDto>> GetUserByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
