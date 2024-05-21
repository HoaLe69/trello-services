using trello_services.Entities;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface ICardRepository
    {
        Task<CardResponseVM> CreateNewCardAsync(CardRequestModel request);
        Task<IList<CardResponseVM>> GetListCardByListIdAsync(Guid listId);
        Task<Card> GetCardDetail(Guid cardId);
        Task ChangeListOfCard(Guid cardId , CardRequestModel request);
        Task<Card> FindCardAsync(Guid cardId);
        Task<Card> UpdateCardAsync(CardRequestModel request , Guid cardId);
        Task<Card> UpdateTimeOfCardAsync(DateTime? starDate, DateTime? endDate , Guid cardId);
        Task<Card> RemoveTimeOfCardAsync(Guid cardId);
        Task MarkDueDateCompleteOrNotAsync(bool isDueDateComplete , Guid cardId); 
    }
}
