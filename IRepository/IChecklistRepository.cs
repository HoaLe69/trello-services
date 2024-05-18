using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface IChecklistRepository
    {
        Task<CheckList> CreateCheckListAsync(string name, Guid cardId);
        Task<CheckList> UpdateNameCheckListAsync(Guid id , string name);
        Task DeleteCheckListAsync(Guid id );
    }
}
