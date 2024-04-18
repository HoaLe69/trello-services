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
                title = request.title,
                boardId  = request.boardId,
            };
            await _context.Columns.AddAsync(column);
            await _context.SaveChangesAsync();
            return column;
        }

        public Task<ListCard> OrderCardInColumnAsync(string id, string cardOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ListCard> UpdateTitleColumnAsync(Int64 id, string title)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null) return null;
            column.title = title;
            await _context.SaveChangesAsync();
            return column;
        }

       
    }
}
