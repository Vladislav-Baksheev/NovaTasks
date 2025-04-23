using Microsoft.AspNetCore.Mvc;
using NovaTasks.Business.Services;
using NovaTasks.Models;

namespace NovaTasks.Controllers;

public class WorkspaceController(WorkspaceService workspaceService) : ControllerBase
{
    [Route("/workspaces")]
    [HttpGet]
    public async Task<List<Workspace>> GetWorkspaces()
    {
        var entities = await workspaceService.GetAllWorkspaces();
        return entities.ToList();
    }
    
    [Route("/addworkspace")]
    [HttpPost]
    public async Task<Workspace> AddWorkspace([FromBody]Workspace workspace)
    {
        var entity = await workspaceService.AddWorkspace(workspace);
        return entity;
    }
}