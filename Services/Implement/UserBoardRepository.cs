using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class UserBoardRepository : IUserBoardRepository
    {
        private readonly ApplicationDBContext _context;
        public UserBoardRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<UserBoard> AddUserToBoardAsync(UserBoard user)
        {
            await _context.UserBoards.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IList<UserResponseVM>> GetListUserOfBoardAsync(Guid boardId)
        {
            var users = await _context.UserBoards.Include(ub => ub.User)
                                                .Where(ub => ub.boardId == boardId)
                                                .Select(s => new UserResponseVM
                                                {
                                                    userId = s.userId,
                                                    avatar_path = s.User.avatar_path,
                                                    displayName = s.User.displayName,
                                                    email = s.User.email
                                                }).ToListAsync();
            return users;
        }

        public async Task RemoveUserFromBoardAsync(Guid userId, Guid boardId)
        {
            var user = await _context.UserBoards
                                    .Where(ub => ub.userId == userId && ub.boardId == boardId)
                                    .SingleOrDefaultAsync();
            _context.UserBoards.Remove(user);
            await _context.SaveChangesAsync();  
        }

        public async Task<UserBoard> UpdateRoleOfUserAsync(Guid userId, Guid boardId, Role? role)
        {
            var user = await _context.UserBoards
                                    .Where(ub => ub.userId == userId && ub.boardId == boardId)
                                    .SingleOrDefaultAsync();
            if (user  == null)  return null;
            user.role = (Role)role;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
