using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Request;
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

        public async Task AddBoardToFavouriteListUserAsync(Guid userId, Guid boardId , bool star)
        {
            var user_board = await _context.UserBoards
                                   .Where(ub => ub.userId == userId && ub.boardId == boardId)
                                   .SingleOrDefaultAsync();
            user_board.star = star;
            await _context.SaveChangesAsync();
        }

        public async Task<UserResponseVM> AddUserToBoardAsync(UserBoardRequestModel request)
        {
            var user_board = new UserBoard
            {
                userId = request.userId,
                boardId = request.boardId,
                role = (Role)request.role
            };
            await _context.UserBoards.AddAsync(user_board);
            await _context.SaveChangesAsync();
            var user = await _context.Users.FindAsync(request.userId);
            return new UserResponseVM
            {
                userId = user.userId,
                displayName = user.displayName,
                avatar_path = user.avatar_path,
                email = user.email
            };
        }

        public async Task<IList<UserBoard>> GetBoardByUserId(Guid userId)
        {
            var boards = await _context.UserBoards.Include(b => b.Board).Where(b =>b.userId  == userId && b.role == Role.Member).ToListAsync();
            return boards;
        }

        public async Task<IList<UserBoard>> GetListUserOfBoardAsync(Guid boardId)
        {
            var users = await _context.UserBoards.Include(ub => ub.User)
                                                .Where(ub => ub.boardId == boardId)
                                                .ToListAsync();
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
