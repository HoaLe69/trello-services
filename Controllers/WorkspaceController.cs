﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        public WorkspaceController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkspaceById(Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest();
                return Ok(await _workspaceRepository.GetWorkspaceByIdAsync(id));

            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{workspaceId}/detail")]
       public async Task<IActionResult> GetWorkSpaceDetailById (Guid workspaceId) 
        {
          
                if (!ValidGuid.IsValidGuid(workspaceId.ToString())) return BadRequest();
                var boards = await _workspaceRepository.GetBoardsByWorkspaceId(workspaceId);
                return Ok(new { success = true , data = boards}) ;
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewWorkSpace(WorkspaceRequestModel request) {
            try
            {
                Console.WriteLine(request);
                if (request == null)
                    return BadRequest(new { message = "Invalid Output" });
                var workspace = await _workspaceRepository.CreateWorkspaceAsync(request);
                return StatusCode(StatusCodes.Status201Created, new { message = "create success", data = workspace });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkspace (WorkspaceRequestModel request , Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString()) || request == null) return BadRequest();
                var workspaceUpdated = await _workspaceRepository.UpdateWorkspaceAsync(request , id);
                return Ok(workspaceUpdated);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace(Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest();
                  await _workspaceRepository.DeleteWorkspaceAsync(id);
                return Ok(new {message = "success"});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }

    }
}
