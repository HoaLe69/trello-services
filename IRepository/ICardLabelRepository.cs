using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface ICardLabelRepository
    {
        Task<IList<CardLabel>> GetAllLabelInCardByCardIdAsync(Guid cardId);
        Task<CardLabel> AddLabelToCardAsync(Guid cardId, Guid labelId);
        Task RemoveLabelFromCardAsync(Guid cardId, Guid labelId);
    }
}
