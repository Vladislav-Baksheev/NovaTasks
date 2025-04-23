using Microsoft.EntityFrameworkCore;
using NovaTasks.Data;
using NovaTasks.Models;
using Task = System.Threading.Tasks.Task;

namespace NovaTasks.Business.Services;

public class BoardService (ApplicationContext context)
{
    public async Task<List<Board>> GetBoards(Workspace workspace)
    {
        var boards = await context.Boards
            .Where(x => x.WorkspaceId == workspace.WorkspaceId)
            .ToListAsync();
        
        return boards;
    }

    public async Task<Board> AddBoard(Board board,  Workspace workspace)
    {
        var entity = new Board
        {
            WorkspaceId = workspace.WorkspaceId,
            Name = board.Name,
        };
        
        await context.Boards.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity;
    }
    
    public async Task DeleteBoard(Board board)
    {
        var entity = await context.Boards
            .FirstOrDefaultAsync(w => w.BoardId == board.BoardId);

        if (entity == null)
        {
            throw new Exception("Board not found");
        }
        context.Boards.Remove(entity);
        
        await context.SaveChangesAsync();
    }
    
    public async Task EditBoard(Board board)
    {
        var entity = await context.Boards
            .FirstOrDefaultAsync(w => w.BoardId == board.BoardId);

        if (entity == null)
        {
            throw new Exception("Board not found");
        }

        // Обновление полей
        entity.Name = board.Name;
        
        await context.SaveChangesAsync();
    }
}