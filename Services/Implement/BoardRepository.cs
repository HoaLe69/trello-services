using Microsoft.EntityFrameworkCore;
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
        public async Task<Board> CreateNewBoardAsync(BoardRequestModel request)
        {
            var board = new Board
            {
                boardId = Guid.NewGuid(),
                workSpaceId = (Guid)request.workSpaceId,
                title = request.title,
                background = request.background,
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

        public async Task<IList<Board>> GetAllBoardsByWoskspaceIdAsync(Guid workspaceId)
        {
            var boards = await _context.Boards.Include(b => b.WorkSpace)
                                                .Where(b => b.WorkSpace.workSpaceId == workspaceId)
                                                .Select(s => new Board { 
                                                    boardId = s.boardId,
                                                    workSpaceId = s.workSpaceId,
                                                    title = s.title,
                                                    orderColumnIds = s.orderColumnIds,
                                                    background = s.background,
                                                })
                                                .ToListAsync();
            return boards;
        }

        public async Task<Board> GetBoardDetailByID(Guid boardId)
        {
            var board = await _context.Boards
                                        .Include(b => b.Columns)
                                        .Where(b => b.boardId == boardId)
                                        .SingleOrDefaultAsync();
            return board;
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
