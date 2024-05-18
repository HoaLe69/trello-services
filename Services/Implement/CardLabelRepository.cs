using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;

namespace trello_services.Services.Implement
{
    public class CardLabelRepository : ICardLabelRepository
    {
        private readonly ApplicationDBContext _context;
        public CardLabelRepository(ApplicationDBContext context) {
            _context = context;
        }
        public async Task<CardLabel> AddLabelToCardAsync(Guid cardId, Guid labelId)
        {
            var card_label = new CardLabel
            {
                cardId = cardId,
                labelId = labelId
            };
            await _context.CardLabels.AddAsync(card_label);
            await _context.SaveChangesAsync();
            return card_label;
            
        }

        public async Task<IList<CardLabel>> GetAllLabelInCardByCardIdAsync(Guid cardId)
        {
            var labels = await _context.CardLabels.Include(cl => cl.Card)
                                                    .Where(cl => cl.cardId == cardId)
                                                    .ToListAsync();
            return labels;
        }

        public async Task RemoveLabelFromCardAsync(Guid cardId, Guid labelId)
        {
            var card_label = await _context.CardLabels.Where(cl => cl.cardId == cardId && cl.labelId == labelId)
                                                        .SingleOrDefaultAsync();
            _context.CardLabels.Remove(card_label);
            await _context.SaveChangesAsync();
        }
    }
}
