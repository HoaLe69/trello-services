using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface IChecklistDetailRepository
    {
        Task<CheckListDetail> CreateCheckListItemAsync(string content, Guid checkListId);
        Task<CheckListDetail> UpdateChecklistItemContentAsync(Int64 id , string content);
        Task DeleteListItemAsync(Int64 id);
        Task UpdateStatusOfListItemAsync(Int64 id, bool status);
        Task<IList<CheckListDetail>> GetAllCheckListDetailAsync(Guid checkId);

    }
}
