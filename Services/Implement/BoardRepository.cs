using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Services.Implement
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDBContext _context;
        public BoardRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task AddBoardToFavouriteList(Guid boardId, bool star)
        {
            var board = await _context.Boards.FindAsync(boardId);
            await _context.SaveChangesAsync();
        }

        public async Task<Board> CreateNewBoardAsync(BoardRequestModel request)
        {
            var board = new Board
            {
                boardId = Guid.NewGuid(),
                workSpaceId = (Guid)request.workSpaceId,
                title = request.title,
                orderColumnIds =  request.orderColumnIds,
            };
            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task DeleteBoardAsync(Guid boardId)
        {
           var board = await _context.Boards.FindAsync(boardId);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateTitleBoardAsync(Guid boardId, string title)
        {
            var board = await _context.Boards.FindAsync(boardId);
            board.title = title;
            await _context.SaveChangesAsync();
            return board.title;
        }
    }
}
