using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Services.Implement
{
    public class LabelRepository : ILabelRepository
    {
        private readonly ApplicationDBContext _context;
        public LabelRepository(ApplicationDBContext context) {
            _context = context;
        }
        public async Task<Label> CreateNewLabelAsync(LabelRequestModel request)
        {
            var label = new Label
            {
                labelId = Guid.NewGuid(),
                theme = request.theme,
                labelName = request.labelName,
                boardId = request.boardId,
            };
            await _context.Labels.AddAsync(label);
            await _context.SaveChangesAsync();
            return label;
        }

        public async Task DeleteLabelAsync(Guid labelId)
        {
            var label = await _context.Labels.FindAsync(labelId);
            if (label != null)
            {
                _context.Labels.Remove(label);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Label>> GetLabelByBoarId(Guid boarId)
        {
            var labels = await _context.Labels
                                        .Include(l => l.Board)
                                        .Where(l => l.boardId == boarId).ToListAsync();
            return labels;
        }

        public async Task<Label> UpdateLabelAsync(Guid labelId, LabelRequestModel update)
        {
            var label = await _context.Labels.FindAsync(labelId);
            if (label == null) return null;
            if (update.theme != null) label.theme = update.theme;
            if (update.labelName != null) label.labelName = update.labelName;
            await _context.SaveChangesAsync();
            return label;

        }
        public async Task<Label> GetById(Guid labelId)
        {
            var label = await _context.Labels.FindAsync(labelId);
            return label;

        }
    }
}
