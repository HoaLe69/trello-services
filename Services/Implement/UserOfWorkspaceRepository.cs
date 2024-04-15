using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class UserOfWorkspaceRepository : IUserOfWorkspaceRepository
    {
        private readonly ApplicationDBContext _context;
        public UserOfWorkspaceRepository(ApplicationDBContext context) { 
            _context = context;
        }
        public async Task<UserWorkspace> AddUserToWorkSpaceAsync(Guid workspaceId, Guid userId, Role? role = Role.Member)
        {
            var user_workspace = new UserWorkspace
            {
                workSpaceId = workspaceId,
                userId = userId,
                role = (Role)role,
            };
            await _context.UserWorkspaces.AddAsync(user_workspace);
            await _context.SaveChangesAsync();
            return user_workspace;
        }

        public async Task RemoveUserFromWorkSpaceAsync(Guid userId, Guid workspaceId)
        {
            var user_workspace = await _context.UserWorkspaces
                                                .Where(uw => uw.userId == userId && uw.workSpaceId == workspaceId)
                                                .SingleOrDefaultAsync();
             _context.UserWorkspaces.Remove(user_workspace);
            await _context.SaveChangesAsync();
        }

        public async Task<UserWorkspace?> FindByUserAndWorkspaceIdAsync(Guid userId, Guid workspaceId)
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

        public async Task<UserWorkspace> UpdateRoleOfUserAsync(Guid userId, Guid workspaceId , Role role)
        {
            var user_workspace = await _context.UserWorkspaces
                                                    .Where(uw => uw.workSpaceId == workspaceId && uw.userId == userId)
                                                    .SingleOrDefaultAsync();
            if (user_workspace == null) { return null; }
            user_workspace.role = role;
            await _context.SaveChangesAsync();
            return user_workspace;
        }

        public async Task<ICollection<UserWorkspace>> GetWorkSpaceOfUserByUserIdAsync(Guid userId)
        {
            var workspace = await _context.UserWorkspaces
                                .Include(w => w.WorkSpace)
                                .Where(uw => uw.userId == userId).ToListAsync();
            return workspace;
        }

        public async Task<IList<UserResponseVM>> GetMemberOfWorkspaceByWorkspaceIdAsync(Guid workspaceId)
        {
            var members = await _context.UserWorkspaces
                                        .Include(uw => uw.User)
                                        .Where(uw => uw.workSpaceId == workspaceId)
                                        .Select(uw => new UserResponseVM
                                        {
                                            userId = uw.userId,
                                            displayName = uw.User.displayName,
                                            email = uw.User.email,
                                            avatar_path = uw.User.avatar_path,
                                        })
                                        .ToListAsync();
            return members;
        }
    }
}
