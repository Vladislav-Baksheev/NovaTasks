using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaTasks.Data;
using NovaTasks.Models;
using Task = System.Threading.Tasks.Task;

namespace NovaTasks.Business.Services;

public class WorkspaceService(
    ApplicationContext context,
    RequestEnvironment environment)
{
    public async Task<List<Workspace>> GetAllWorkspaces()
    {
        var workspaces = await context.Workspaces
            //.IsUserEntity(environment.UserId)
            .ToListAsync();
        return workspaces;
    }

    public async Task<Workspace> AddWorkspace(Workspace workspace)
    {
        var entity = new Workspace
        {
            WorkspaceId =  workspace.WorkspaceId,
            Name = workspace.Name
        };
        await context.Workspaces.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity;
    }
    
    public async Task DeleteWorkspace(Workspace workspace)
    {
        var entity = await context.Workspaces
            .FirstOrDefaultAsync(w => w.WorkspaceId == workspace.WorkspaceId);

        if (entity == null)
        {
            throw new Exception("Workspace not found");
        }
        context.Workspaces.Remove(entity);
        
        await context.SaveChangesAsync();
    }
    
    public async Task EditWorkspace(Workspace workspace)
    {
        var entity = await context.Workspaces
            .FirstOrDefaultAsync(w => w.WorkspaceId == workspace.WorkspaceId);

        if (entity == null)
        {
            throw new Exception("Workspace not found");
        }

        // Обновление полей
        entity.Name = workspace.Name;
        entity.Visibility = workspace.Visibility;
        
        await context.SaveChangesAsync();
    }
}