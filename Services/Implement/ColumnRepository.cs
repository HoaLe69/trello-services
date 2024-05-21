using Microsoft.EntityFrameworkCore.Metadata.Internal;
using trello_services.Data;
using trello_services.IRepository;
using trello_services.Models.Request;
using trello_services.Entities;

namespace trello_services.Services.Implement
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly ApplicationDBContext _context;
        public ColumnRepository(ApplicationDBContext context) { 
            _context =  context;    
        }
        public async Task<ListCard> CreateNewColumnAsync(ListCardRequestModel request)
        {
            var column = new ListCard
            {
                columnId = Guid.NewGuid(),
                title = request.title,
                boardId  = (Guid)request.boardId,
            };
            await _context.Columns.AddAsync(column);
            await _context.SaveChangesAsync();
            return column;
        }

        public async Task OrderCardInColumnAsync(Guid id, ListCardRequestModel requestModel)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null) return;
            column.orderCardIds = requestModel.orderCardIds;
            await _context.SaveChangesAsync();
        }

        public async Task<ListCard> UpdateTitleColumnAsync(Guid id, string title)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null) return null;
            column.title = title;
            await _context.SaveChangesAsync();
            return column;
        }

       
    }
}
