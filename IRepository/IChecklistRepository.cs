using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface IChecklistRepository
    {
        Task<CheckList> CreateCheckListAsync(string name, Int64 cardId);
        Task<CheckList> UpdateNameCheckListAsync(Guid id , string name);
        Task DeleteCheckListAsync(Guid id );
    }
}
