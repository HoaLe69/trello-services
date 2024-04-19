using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;

namespace trello_services.Services.Implement
{
    public class ChecklistRepository : IChecklistRepository
    {
        private readonly ApplicationDBContext _context;
        public ChecklistRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<CheckList> CreateCheckListAsync(string name, long cardId)
        {
            var checklist = new CheckList {
                checkListId = Guid.NewGuid(),
                cardId = cardId,
                title = name
            };
            await _context.CheckLists.AddAsync(checklist);  
            await _context.SaveChangesAsync();
            return checklist;

        }

        public async Task DeleteCheckListAsync(Guid id)
        {
            var checklist = await _context.CheckLists.FindAsync(id);
            _context.CheckLists.Remove(checklist);
            await _context.SaveChangesAsync();  
        }

        public async Task<CheckList> UpdateNameCheckListAsync(Guid id , string name)
        {
            var checklist = await _context.CheckLists.FindAsync(id);
            if (checklist == null) return null;
            checklist.title = name;
            await _context.SaveChangesAsync();
            return checklist;
        }
    }
}
