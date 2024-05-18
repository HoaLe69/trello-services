using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class UserCardRepository : IUserCardRepository
    {
        private readonly ApplicationDBContext _context;
        public UserCardRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<UserCard> AddUserToCardAsync(Guid userId, Guid cardId)
        {
            var user_card = new UserCard
            {
                userId = userId,
                cardId = cardId
            };
            await _context.UserCards.AddAsync(user_card);
            await _context.SaveChangesAsync();
            return user_card;

        }

        public async Task<IList<UserResponseVM>> GetListUserInCardAsync(Guid userId)
        {
            var user_card = await _context.UserCards.Include(uc => uc.User)
                                                    .Where(uc => uc.userId == userId)
                                                    .Select(s => new UserResponseVM
                                                    {
                                                        userId = s.userId,
                                                        displayName = s.User.displayName,
                                                        email = s.User.email,
                                                        avatar_path = s.User.avatar_path
                                                    })
                                                    .ToListAsync();
            return user_card;
        }

        public async Task RemoveUserFromCardAsync(Guid userId, Guid cardId)
        {
            var user_card = await _context.UserCards.Where(uc => uc.userId == userId && uc.cardId == cardId)
                                                    .SingleOrDefaultAsync();
            if (user_card != null)
            {
                _context.UserCards.Remove(user_card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
