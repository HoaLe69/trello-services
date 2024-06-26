﻿using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Services.Implement
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ApplicationDBContext _context;
        public WorkspaceRepository(ApplicationDBContext context)
        {
            _context = context; 
        }
        public async Task<WorkSpace> CreateWorkspaceAsync(WorkspaceRequestModel request)
        {
            var workspace = new WorkSpace
            {
                workSpaceId = Guid.NewGuid(),
                theme = request.theme,
                title = request.title,
                description = request.description,
            };
            await _context.Workspaces.AddAsync(workspace);
            await _context.SaveChangesAsync();
            return workspace;
        }

       

        public async Task<WorkSpace?> UpdateWorkspaceAsync(WorkspaceRequestModel request , Guid id)
        {
            var workspace = await _context.Workspaces.FindAsync(id);
            if (workspace == null) { return null; }
            if (request.title != null) workspace.title = request.title;
            if (request.theme != null) workspace.theme = request.theme;
            if (request.description != null) workspace.description = request.description;
            await _context.SaveChangesAsync();
            return workspace;
        }
        public  async Task DeleteWorkspaceAsync (Guid workspaceId)
        {
            var workspace  = await _context.Workspaces.FindAsync(workspaceId);
            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();
        }
        public async Task<WorkSpace?> GetWorkspaceByIdAsync (Guid id)
        {
            var workspace = await _context.Workspaces.FindAsync(id);
            return workspace;
        }

        public async Task<WorkSpace> GetBoardsByWorkspaceId(Guid workspaceId)
        {
            var workspace = await _context.Workspaces.Include(w => w.Boards)
                                                      .Where(w => w.workSpaceId == workspaceId)
                                                      .SingleOrDefaultAsync();
                                                      
            return workspace;
        }   
    }
}
