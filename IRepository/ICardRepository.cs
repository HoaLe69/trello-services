using trello_services.Entities;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface ICardRepository
    {
        Task<CardResponseVM> CreateNewCardAsync(CardRequestModel request);
        Task<IList<CardResponseVM>> GetListCardByListIdAsync(Int64 listId);
        Task<Card> FindCardAsync(Int64 cardId);
        Task<Card> UpdateCardAsync(CardRequestModel request , Int64 cardId);
        Task<Card> UpdateTimeOfCardAsync(DateTime? starDate, DateTime? endDate , Int64 cardId);
        Task<Card> RemoveTimeOfCardAsync(Int64 cardId);
        Task MarkDueDateCompleteOrNotAsync(bool isDueDateComplete , Int64 cardId); 
    }
}
