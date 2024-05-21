using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface IBoardRepository
    {
        Task<Board> CreateNewBoardAsync(BoardRequestModel request);
        Task<string> UpdateTitleBoardAsync(Guid boardId ,  BoardRequestModel title);
        Task DeleteBoardAsync(Guid boardId);
        Task<IList<Board>> GetAllBoardsByWoskspaceIdAsync(Guid workspaceId);
        Task<Board> GetBoardDetailByID(Guid boardId);
         Task UpdateOrderColumnInBoard(Guid boardId, BoardRequestModel request);
    }
}
