using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;

namespace trello_services.Services.Implement
{
    public class UserOfWorkspaceRepository : IUserOfWorkspaceRepository
    {
        private readonly ApplicationDBContext _context;
        public UserOfWorkspaceRepository(ApplicationDBContext context) { 
            _context = context;
        }
        public async Task<UserWorkspace> AddUserToWorkSpace(Guid userId, Guid workspaceId , Role? role = Role.Member)
        {
            var user_workspace = new UserWorkspace
            {
                userId = userId,
                workSpaceId = workspaceId,
                role = (Role)role,
            };
            await _context.UserWorkspaces.AddAsync(user_workspace);
            await _context.SaveChangesAsync();
            return user_workspace;
        }

        public void RemoveUserFromWorkSpace(Guid userId, Guid workspaceId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserWorkspace?> FindByUserAndWorkspaceId(Guid userId, Guid workspaceId)
        {
            var user_workspace = await _context.UserWorkspaces
                                                .Where(uw => uw.userId ==  userId && uw.workSpaceId == workspaceId)
                                                .SingleOrDefaultAsync();
            if (user_workspace == null)
            {
                return null;
            }
            return user_workspace;
        }

        public void UpdateRoleOfUser(Guid userId, Guid workspaceId)
        {
            throw new NotImplementedException();
        }
    }
}
