using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
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

        public async Task<IList<WorkSpace>> GetAllByUserIdAsync(Guid id)
        {
            var workspaces = await _context.Workspaces
                                            .Include(w => w.UserWorkspaces.Where(uw => uw.userId == id))
                                            .ToListAsync();
            return workspaces;  
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
        public  async void DeleteWorkspaceAsync (WorkSpace workspace)
        {
            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync(true);
        }
        public async Task<WorkSpace?> GetWorkspaceByIdAsync (Guid id)
        {
            var workspace = await _context.Workspaces.FindAsync(id);
            return workspace;
        }
    }
}
