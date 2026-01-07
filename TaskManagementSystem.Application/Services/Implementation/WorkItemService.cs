using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Application.Mapping;
using TaskManagementSystem.Application.Services.Interface;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Interface.Repositories;

namespace TaskManagementSystem.Application.Services.Implementation
{
    public class WorkItemService : IWorkItemService
    {
        private readonly ILogger<WorkItemService> _logger;
        private readonly IRepository<WorkItem> _repository;
        private readonly IRepository<User> _userRepository;

        public WorkItemService(ILogger<WorkItemService> logger,
                               IRepository<WorkItem> repository,
                               IRepository<User> userRepository)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
        }

        //--------------------------------------------------------*
        public async Task<PagedDataDto<WorkItemResponse>> Search(SearchPageDto<WorkItemFilter> searchPageDto, int loggedInUserId, UserRole loggedInUserRole)
        {
            _logger.LogInformation($"WorkItemService - Search | Start");

            Expression<Func<WorkItem, bool>> predicate = x =>
            (
                string.IsNullOrEmpty(searchPageDto.Criteria.Title) ||
                x.Title.Trim().ToUpper().Contains(searchPageDto.Criteria.Title.Trim().ToUpper())
            ) &&
            (
                string.IsNullOrEmpty(searchPageDto.Criteria.ReferenceCode) ||
                x.ReferenceCode.Trim().ToUpper().Contains(searchPageDto.Criteria.ReferenceCode.Trim().ToUpper())
            ) &&
            (
                !searchPageDto.Criteria.AssignedUserId.HasValue ||
                (x.AssignedUserId.HasValue && x.AssignedUserId == searchPageDto.Criteria.AssignedUserId.Value)
            ) &&
            (
                !searchPageDto.Criteria.Status.HasValue ||
                x.Status == searchPageDto.Criteria.Status.Value
            ) &&
            (
                loggedInUserRole != UserRole.User ||
                (x.AssignedUserId.HasValue && x.AssignedUserId == loggedInUserId)
            );

            var workItems = await _repository.PageAsync(predicate: predicate,
                                                        pageIndex: searchPageDto.PageIndex,
                                                        pageSize: searchPageDto.PageSize);

            _logger.LogInformation($"WorkItemService - Search | End ReturnedCount={workItems.Data.Count}, TotalCount={workItems.TotalCount}");

            var result = new PagedDataDto<WorkItemResponse>
            {
                Data = workItems.Data.Select(WorkItem => WorkItem.From()).ToList(),
                TotalCount = workItems.TotalCount
            };

            return result;
        }

        public async Task<WorkItemResponse> GetById(int id, int loggedInUserId, UserRole loggedInUserRole)
        {
            _logger.LogInformation($"WorkItemService - GetById | Start Id={id}");

            if (id <= 0)
            {
                _logger.LogWarning($"WorkItemService - GetById | Invalid Id={id}");
                throw new Exception("Invalid ID");
            }

            var workItem = await _repository.GetByIdAsync(id: id);

            if (workItem is null)
            {
                _logger.LogWarning($"WorkItemService - GetById | WorkItem not found Id={id}");
                throw new Exception("WorkItem not found");
            }

            // Authorization check
            if (loggedInUserRole == UserRole.User && (!workItem.AssignedUserId.HasValue || workItem.AssignedUserId != loggedInUserId))
            {
                _logger.LogWarning($"WorkItemService - GetById | Access denied Id={id}, LoggedInUserId={loggedInUserId}");
                throw new Exception("You are not allowed to access this work item");
            }

            _logger.LogInformation($"WorkItemService - GetById | End Id={id}");

            return workItem.From();
        }

        public async Task Add(CreateWorkItemRequest request, int loggedInUserId)
        {
            _logger.LogInformation($"WorkItemService - Add | Start Title={request.Title}, LoggedInUserId={loggedInUserId}");

            if (request.AssignedUserId.HasValue &&
                request.AssignedUserId.Value > 0 &&
                !await _userRepository.AnyAsync(x => x.Id == request.AssignedUserId.Value))
            {
                _logger.LogWarning($"WorkItemService - Add | Assigned user not found Id={request.AssignedUserId}");
                throw new Exception("Assigned user does not exist");
            }

            await _repository.AddAsync(entity: request.To(loggedInUserId: loggedInUserId));

            _logger.LogInformation($"WorkItemService - Add | End Title={request.Title}");
        }

        public async Task Update(UpdateWorkItemRequest request, int loggedInUserId, UserRole loggedInUserRole)
        {
            _logger.LogInformation($"WorkItemService - Update | Start Id={request.Id}, LoggedInUserId={loggedInUserId}");

            if (request.Id <= 0)
            {
                _logger.LogWarning("WorkItemService - Update | Invalid Id");
                throw new Exception("Invalid ID");
            }

            if (request.AssignedUserId.HasValue &&
                request.AssignedUserId.Value > 0 &&
                !await _userRepository.AnyAsync(x => x.Id == request.AssignedUserId.Value))
            {
                _logger.LogWarning($"WorkItemService - Update | Assigned user not found Id={request.AssignedUserId}");

                throw new Exception("Assigned user does not exist");
            }

            var workItem = await _repository.GetByIdAsync(id: request.Id);

            if (workItem is null)
            {
                _logger.LogWarning($"WorkItemService - Update | WorkItem not found Id={request.Id}");
                throw new Exception("WorkItem not found");
            }

            // Authorization check
            if (loggedInUserRole == UserRole.User && (!workItem.AssignedUserId.HasValue || workItem.AssignedUserId != loggedInUserId))
            {
                _logger.LogWarning($"WorkItemService - Update | Access denied Id={workItem.Id}, LoggedInUserId={loggedInUserId}");
                throw new Exception("You are not allowed to access this work item");
            }

            workItem.Update(title: request.Title,
                            description: request.Description,
                            status: request.Status,
                            assignedUserId: request.AssignedUserId,
                            loggedInUserId: loggedInUserId);

            _logger.LogInformation($"WorkItemService - Update | End Id={request.Id}");
        }

        public async Task UpdateStatus(int id, WorkItemStatus status, int loggedInUserId, UserRole loggedInUserRole)
        {
            _logger.LogInformation($"WorkItemService - UpdateStatus | Start Id={id}, LoggedInUserId={loggedInUserId}");

            if (id <= 0)
            {
                _logger.LogWarning("WorkItemService - UpdateStatus | Invalid Id");
                throw new Exception("Invalid ID");
            }

            var workItem = await _repository.GetByIdAsync(id: id);

            if (workItem is null)
            {
                _logger.LogWarning($"WorkItemService - UpdateStatus | WorkItem not found Id={id}");
                throw new Exception("WorkItem not found");
            }

            // Authorization check
            if (loggedInUserRole == UserRole.User && (!workItem.AssignedUserId.HasValue || workItem.AssignedUserId != loggedInUserId))
            {
                _logger.LogWarning($"WorkItemService - UpdateStatus | Access denied Id={workItem.Id}, LoggedInUserId={loggedInUserId}");
                throw new Exception("You are not allowed to access this work item");
            }

            workItem.UpdateStatus(status: status,
                                  loggedInUserId: loggedInUserId);

            _logger.LogInformation($"WorkItemService - UpdateStatus | End Id={id}");
        }

        public async Task Delete(int id, int loggedInUserId, UserRole loggedInUserRole)
        {
            _logger.LogInformation($"WorkItemService - Delete | Start Id={id}, LoggedInUserId={loggedInUserId}");

            if (id <= 0)
            {
                _logger.LogWarning("WorkItemService - Delete | Invalid Id");
                throw new Exception("Invalid ID");
            }

            var workItem = await _repository.GetByIdAsync(id: id);

            if (workItem is null)
            {
                _logger.LogWarning($"WorkItemService - Delete | WorkItem not found Id={id}");
                throw new Exception("WorkItem profile not found");
            }

            // Authorization check
            if (loggedInUserRole == UserRole.User && (!workItem.AssignedUserId.HasValue || workItem.AssignedUserId != loggedInUserId))
            {
                _logger.LogWarning($"WorkItemService - Delete | Access denied Id={workItem.Id}, LoggedInUserId={loggedInUserId}");
                throw new Exception("You are not allowed to access this work item");
            }

            workItem.Delete(loggedInUserId: loggedInUserId);

            _logger.LogInformation($"WorkItemService - Delete | End Id={id}");
        }
    }
}
