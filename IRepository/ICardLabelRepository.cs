using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface ICardLabelRepository
    {
        Task<IList<CardLabel>> GetAllLabelInCardByCardIdAsync(Int64 cardId);
        Task<CardLabel> AddLabelToCardAsync(Int64 cardId, Guid labelId);
        Task RemoveLabelFromCardAsync(Int64 cardId, Guid labelId);
    }
}
