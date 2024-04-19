using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface ICardRepository
    {
        Task<Card> CreateNewCardAsync(CardRequestModel request);
        Task<Card> FindCardAsync(Int64 cardId);
        Task<Card> UpdateCardAsync(CardRequestModel request , Int64 cardId);
        Task<Card> UpdateTimeOfCardAsync(DateTime? starDate, DateTime? endDate , Int64 cardId);
        Task<Card> RemoveTimeOfCardAsync(Int64 cardId);
        Task MarkDueDateCompleteOrNotAsync(bool isDueDateComplete , Int64 cardId); 
    }
}
