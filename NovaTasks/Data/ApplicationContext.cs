using Microsoft.EntityFrameworkCore;
using NovaTasks.Models;
using Task = NovaTasks.Models.Task;
namespace NovaTasks.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<TasksList> TasksLists { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }
    public DbSet<TaskAssignee> TaskAssignees { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<TaskLabel> TaskLabels { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Workspace>().ToTable("workspaces");
        modelBuilder.Entity<Board>().ToTable("boards");
        modelBuilder.Entity<TasksList>().ToTable("tasks_list");
        modelBuilder.Entity<Task>().ToTable("tasks");
        modelBuilder.Entity<WorkspaceMember>().ToTable("workspace_members");
        modelBuilder.Entity<TaskAssignee>().ToTable("task_assignees");
        modelBuilder.Entity<Label>().ToTable("labels");
        modelBuilder.Entity<TaskLabel>().ToTable("task_labels");

        // Конфигурация связей
        ConfigureWorkspaceRelations(modelBuilder);
        ConfigureTaskRelations(modelBuilder);
        ConfigureUserRelations(modelBuilder);
        ConfigureIndexes(modelBuilder);
    }

    private void ConfigureWorkspaceRelations(ModelBuilder modelBuilder)
    {
        // Workspace (1) → (N) Board
        modelBuilder.Entity<Board>()
            .HasOne(b => b.Workspace)
            .WithMany(w => w.Boards)
            .HasForeignKey(b => b.WorkspaceId)
            .HasConstraintName("fk_boards_workspaces");

        // Board (1) → (N) TasksList
        modelBuilder.Entity<TasksList>()
            .HasOne(tl => tl.Board)
            .WithMany(b => b.TasksLists)
            .HasForeignKey(tl => tl.BoardId)
            .HasConstraintName("fk_tasks_list_boards");
    }

    private void ConfigureTaskRelations(ModelBuilder modelBuilder)
    {
        // TasksList (1) → (N) Task
        modelBuilder.Entity<Task>()
            .HasOne(t => t.TasksList)
            .WithMany(tl => tl.Tasks)
            .HasForeignKey(t => t.TasksListId)
            .HasConstraintName("fk_tasks_tasks_list");

        // Task (N) ↔ (N) Label (через TaskLabel)
        modelBuilder.Entity<TaskLabel>()
            .HasKey(tl => new { tl.TaskId, tl.LabelId });

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Task)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskId)
            .HasConstraintName("fk_task_labels_tasks");

        modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelId)
            .HasConstraintName("fk_task_labels_labels");
    }

    private void ConfigureUserRelations(ModelBuilder modelBuilder)
    {
        // User (N) ↔ (N) Workspace (через WorkspaceMember)
        modelBuilder.Entity<WorkspaceMember>()
            .HasKey(wm => new { wm.WorkspaceId, wm.UserId });

        modelBuilder.Entity<WorkspaceMember>()
            .HasOne(wm => wm.User)
            .WithMany(u => u.WorkspaceMemberships)
            .HasForeignKey(wm => wm.UserId)
            .HasConstraintName("fk_workspace_members_users");

        modelBuilder.Entity<WorkspaceMember>()
            .HasOne(wm => wm.Workspace)
            .WithMany(w => w.Members)
            .HasForeignKey(wm => wm.WorkspaceId)
            .HasConstraintName("fk_workspace_members_workspaces");

        // User (N) ↔ (N) Task (через TaskAssignee)
        modelBuilder.Entity<TaskAssignee>()
            .HasKey(ta => new { ta.TaskId, ta.UserId });

        modelBuilder.Entity<TaskAssignee>()
            .HasOne(ta => ta.User)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(ta => ta.UserId)
            .HasConstraintName("fk_task_assignees_users");
    }

    private void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // Индексы для часто используемых полей
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ix_users_email");

        modelBuilder.Entity<Task>()
            .HasIndex(t => t.TasksListId)
            .HasDatabaseName("ix_tasks_list_id");

        modelBuilder.Entity<Task>()
            .HasIndex(t => t.EndDate)
            .HasDatabaseName("ix_tasks_due_date");
    }
}