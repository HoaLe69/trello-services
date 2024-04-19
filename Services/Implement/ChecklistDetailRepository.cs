using Azure.Core;
using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;

namespace trello_services.Services.Implement
{
    public class ChecklistDetailRepository : IChecklistDetailRepository
    {
        private readonly ApplicationDBContext _context;
        public ChecklistDetailRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<CheckListDetail> CreateCheckListItemAsync(string content, Guid checkListId)
        {
            var item = new CheckListDetail { 
                content = content,
                checkListId = checkListId
            };
            await _context.CheckListDetails.AddAsync(item);
            await _context.SaveChangesAsync();  
            return item;

        }

        public async Task DeleteListItemAsync(long id)
        {
            var item = await _context.CheckListDetails.FindAsync(id);
            if (item != null)
            {
                _context.CheckListDetails.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<CheckListDetail>> GetAllCheckListDetailAsync(Guid checkId)
        {
            var listItems = await _context.CheckListDetails.Include(cld => cld.CheckList)
                                                            .Where(cld => cld.checkListId == checkId)
                                                            .ToListAsync(); 
                return listItems;                   
        }

        public async Task<CheckListDetail> UpdateChecklistItemContentAsync(long id, string content)
        {
            var item = await _context.CheckListDetails.FindAsync(id);
            if (item != null )
            {
                item.content = content;
                await _context.SaveChangesAsync();
                return item;
            }
            return null;
        }

        public async Task UpdateStatusOfListItemAsync(long id, bool status)
        {
            var item = await _context.CheckListDetails.FindAsync(id);
            if (item != null)
            {
                item.status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
