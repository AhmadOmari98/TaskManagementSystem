using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Attributes;
using TaskManagementSystem.Application.Authorization;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.Services.Interface;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.API.Controllers
{
    public class WorkItemsController : BaseApiController
    {
        private readonly ILogger<WorkItemsController> _logger;
        private readonly IWorkItemService _workItemService;

        public WorkItemsController(ILogger<WorkItemsController> logger,
                                   IWorkItemService workItemService)
        {
            _logger = logger;
            _workItemService = workItemService;
        }

        //----------------------------------------------------------------*
        [HttpPost("search")]
        [RequirePermission(Permissions.WorkItem_Search)]
        public async Task<IActionResult> Search([FromBody] SearchPageDto<WorkItemFilter> searchPageRequest)
        {
            _logger.LogInformation($"WorkItemsController - Search | Start RequestedBy={LoggedInUserId}");

            var result = await _workItemService.Search(searchPageDto: searchPageRequest,
                                                       loggedInUserId: LoggedInUserId,
                                                       loggedInUserRole: LoggedInUserRole);

            _logger.LogInformation($"WorkItemsController - Search | End ReturnedCount={result.Data.Count}, TotalCount={result.TotalCount}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        [RequirePermission(Permissions.WorkItem_View)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"WorkItemsController - GetById | Start Id={id}, RequestedBy={LoggedInUserId}");

            var result = await _workItemService.GetById(id: id,
                                                        loggedInUserId: LoggedInUserId,
                                                        loggedInUserRole: LoggedInUserRole);
             
            _logger.LogInformation($"WorkItemsController - GetById | End Id={id}");

            return Ok(result);
        }

        [HttpPost]
        [RequirePermission(Permissions.WorkItem_Create)]
        public async Task<IActionResult> Create(CreateWorkItemRequest request)
        {
            _logger.LogInformation($"WorkItemsController - Create | Start Title={request.Title}, LoggedInUserId={LoggedInUserId}");

            await _workItemService.Add(request: request, 
                                       loggedInUserId: LoggedInUserId);

            _logger.LogInformation($"WorkItemsController - Create | End Title={request.Title}");

            return Ok();
        }

        [HttpPut]
        [RequirePermission(Permissions.WorkItem_Update)]
        public async Task<IActionResult> Update(UpdateWorkItemRequest request)
        {
            _logger.LogInformation($"WorkItemsController - Update | Start Id={request.Id}, LoggedInUserId={LoggedInUserId}");

            await _workItemService.Update(request: request,
                                          loggedInUserId: LoggedInUserId,
                                          loggedInUserRole: LoggedInUserRole);

            _logger.LogInformation($"WorkItemsController - Update | End Id={request.Id}");

            return Ok();
        }

        [HttpPatch("{id}/status")]
        [RequirePermission(Permissions.WorkItem_UpdateStatus)]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] WorkItemStatus status)
        {
            _logger.LogInformation($"WorkItemsController - UpdateStatus | Start Id={id}, Status={status}, LoggedInUserId={LoggedInUserId}");

            await _workItemService.UpdateStatus(id: id,
                                                status: status,
                                                loggedInUserId: LoggedInUserId,
                                                loggedInUserRole: LoggedInUserRole);

            _logger.LogInformation($"WorkItemsController - UpdateStatus | End Id={id}");

            return Ok();
        }

        [HttpDelete("{id}")]
        [RequirePermission(Permissions.WorkItem_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"WorkItemsController - Delete | Start Id={id}, LoggedInUserId={LoggedInUserId}");

            await _workItemService.Delete(id: id,
                                          loggedInUserId: LoggedInUserId,
                                          loggedInUserRole: LoggedInUserRole);

            _logger.LogInformation($"WorkItemsController - Delete | End Id={id}");

            return Ok();
        }
    }
}
