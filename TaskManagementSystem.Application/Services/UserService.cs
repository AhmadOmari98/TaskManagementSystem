using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Application.Mapping;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface.Repositories;
using TaskManagementSystem.Domain.Interface.Services;

namespace TaskManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<User> _repository;

        public UserService(ILogger<UserService> logger,
                           IRepository<User> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<PagedDataDto<UserResponse>> Search(SearchPageDto<UserFilter> searchPageDto)
        {
            _logger.LogInformation($"UserService - Search | Start PageIndex={searchPageDto.PageIndex}, PageSize={searchPageDto.PageSize}, Criteria={searchPageDto.Criteria}");

            Expression<Func<User, bool>> predicate = x =>
            (
                string.IsNullOrEmpty(searchPageDto.Criteria.Name) ||
                x.Name.Trim().ToUpper().Contains(searchPageDto.Criteria.Name.Trim().ToUpper())
            ) &&
            (
                string.IsNullOrEmpty(searchPageDto.Criteria.Email) ||
                x.Email.Trim().ToUpper().Contains(searchPageDto.Criteria.Email.Trim().ToUpper())
            ) &&
            (
                !searchPageDto.Criteria.Role.HasValue ||
                x.Role == searchPageDto.Criteria.Role.Value
            );

            var userProfiles = await _repository.PageAsync(
                predicate: predicate,
                pageIndex: searchPageDto.PageIndex,
                pageSize: searchPageDto.PageSize);

            _logger.LogInformation($"UserService - Search | End ReturnedCount={userProfiles.Data.Count}, TotalCount={userProfiles.TotalCount}");

            var result = new PagedDataDto<UserResponse>
            {
                Data = userProfiles.Data.Select(user => user.From()).ToList(),
                TotalCount = userProfiles.TotalCount
            };

            return result;
        }

        public async Task<UserResponse> GetById(int id)
        {
            _logger.LogInformation($"UserService - GetById | Start Id={id}");

            if (id <= 0)
            {
                _logger.LogWarning($"UserService - GetById | Invalid Id={id}");
                throw new Exception("Invalid ID");
            }

            var user = await _repository.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogWarning($"UserService - GetById | User not found Id={id}");
                throw new Exception("User not found");
            }

            _logger.LogInformation($"UserService - GetById | End Id={id}");

            return user.From();
        }

        public async Task Add(CreateUserRequest request, int createdBy)
        {
            _logger.LogInformation($"UserService - Add | Start Email={request.Email}, CreatedBy={createdBy}");

            if (await _repository.AnyAsync(x => request.Email.Trim().ToUpper() == x.Email.Trim().ToUpper()))
            {
                _logger.LogWarning($"UserService - Add | Duplicate email detected Email={request.Email}");
                throw new Exception("Email already exists");
            }

            await _repository.AddAsync(request.To(createdBy));

            _logger.LogInformation($"UserService - Add | End Email={request.Email}");
        }

        public async Task Update(UpdateUserRequest request, int updatedBy)
        {
            _logger.LogInformation($"UserService - Update | Start Id={request.Id}, UpdatedBy={updatedBy}");

            if (request.Id <= 0)
            {
                _logger.LogWarning("UserService - Update | Invalid Id");
                throw new Exception("Invalid ID");
            }

            if (await _repository.AnyAsync(x => x.Id != request.Id && request.Email.Trim().ToUpper() == x.Email.Trim().ToUpper()))
            {
                _logger.LogWarning($"UserService - Update | Duplicate email Email={request.Email}");
                throw new Exception("Email already exists");
            }

            var user = await _repository.GetByIdAsync(request.Id);

            if (user is null)
            {
                _logger.LogWarning($"UserService - Update | User not found Id={request.Id}");
                throw new Exception("User not found");
            }

            user.Update(request.Name, request.Email, request.Role, updatedBy);

            _logger.LogInformation($"UserService - Update | End Id={request.Id}");

        }

        public async Task Delete(int id, int updatedBy)
        {
            _logger.LogInformation($"UserService - Delete | Start Id={id}, DeletedBy={updatedBy}");

            if (id <= 0)
            {
                _logger.LogWarning("UserService - Delete | Invalid Id");
                throw new Exception("Invalid ID");
            }

            var user = await _repository.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogWarning($"UserService - Delete | User not found Id={id}");
                throw new Exception("User profile not found");
            }
            user.Delete(updatedBy);

            _logger.LogInformation($"UserService - Delete | End Id={id}");
        }
    }
}
