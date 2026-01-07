using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Attributes;
using TaskManagementSystem.Application.Authorization;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.DTOs.Filter;
using TaskManagementSystem.Application.DTOs.Request;
using TaskManagementSystem.Application.Services.Interface;

namespace TaskManagementSystem.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger,
                               IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        //----------------------------------------------------------------*

        [HttpPost("search")]
        [RequirePermission(Permissions.User_Search)]
        public async Task<IActionResult> Search([FromBody] SearchPageDto<UserFilter> searchPageRequest)
        {
            _logger.LogInformation($"UsersController - Search | Start RequestedBy={LoggedInUserId}");

            var result = await _userService.Search(searchPageDto: searchPageRequest);

            _logger.LogInformation($"UsersController - Search | End ReturnedCount={result.Data.Count}, TotalCount={result.TotalCount}");

            return Ok(result);
        }


        [HttpGet("{id}")]
        [RequirePermission(Permissions.User_View)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"UsersController - GetById | Start Id={id}, RequestedBy={LoggedInUserId}");

            var result = await _userService.GetById(id: id);

            _logger.LogInformation($"UsersController - GetById | End Id={id}");

            return Ok(result);
        }

        [HttpPost]
        [RequirePermission(Permissions.User_Create)]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            _logger.LogInformation($"UsersController - Create | Start Email={request.Email}, LoggedInUserId={LoggedInUserId}");

            await _userService.Add(request: request, loggedInUserId: LoggedInUserId);

            _logger.LogInformation($"UsersController - Create | End Email={request.Email}");

            return Ok();
        }

        [HttpPut]
        [RequirePermission(Permissions.User_Update)]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            _logger.LogInformation($"UsersController - Update | Start Id={request.Id}, LoggedInUserId={LoggedInUserId}");

            await _userService.Update(request: request, loggedInUserId: LoggedInUserId);

            _logger.LogInformation($"UsersController - Update | End Id={request.Id}");

            return Ok();
        }

        [HttpDelete("{id}")]
        [RequirePermission(Permissions.User_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"UsersController - Delete | Start Id={id}, LoggedInUserId={LoggedInUserId}");

            await _userService.Delete(id: id, loggedInUserId: LoggedInUserId);

            _logger.LogInformation($"UsersController - Delete | End Id={id}");

            return Ok();
        }

    }
}
