using Microsoft.EntityFrameworkCore.Metadata.Internal;
using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface IColumnRepository
    {
        Task<ListCard> CreateNewColumnAsync(ListCardRequestModel request);
        Task<ListCard> UpdateTitleColumnAsync(Int64 id, string title);
        Task<ListCard> OrderCardInColumnAsync(string id, string cardOrder);
    }
}