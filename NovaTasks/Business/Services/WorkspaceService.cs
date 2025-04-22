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

    public async Task<string> AddWorkspace(Workspace workspace)
    {
        var entity = new Workspace
        {
            WorkspaceId =  workspace.WorkspaceId,
            Name = workspace.Name
        };
        await context.Workspaces.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return entity.Name;
    }
}