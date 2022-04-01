using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProjectManager.Models
{
    public partial class ProjectManagerDb : DbContext
    {
        public ProjectManagerDb()
            : base("name=ProjectManagerDb")
        {
        }

        public virtual DbSet<CostTypes> CostTypes { get; set; }
        public virtual DbSet<DateTypes> DateTypes { get; set; }
        public virtual DbSet<MaterialResoucres> MaterialResoucres { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<ProjectResources> ProjectResources { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<ProjectTasks> ProjectTasks { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SalaryTypes> SalaryTypes { get; set; }
        public virtual DbSet<TaskConnections> TaskConnections { get; set; }
        public virtual DbSet<TaskGroups> TaskGroups { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TaskTypes> TaskTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WeekDays> WeekDays { get; set; }
        public virtual DbSet<WorkingCalendars> WorkingCalendars { get; set; }
        public virtual DbSet<WorkingCalendarWeekends> WorkingCalendarWeekends { get; set; }
        public virtual DbSet<WorkingResources> WorkingResources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostTypes>()
                .HasMany(e => e.MaterialResoucres)
                .WithOptional(e => e.CostTypes)
                .HasForeignKey(e => e.CostType);

            modelBuilder.Entity<CostTypes>()
                .HasMany(e => e.SalaryTypes)
                .WithOptional(e => e.CostTypes)
                .HasForeignKey(e => e.CostType);

            modelBuilder.Entity<DateTypes>()
                .HasMany(e => e.SalaryTypes)
                .WithOptional(e => e.DateTypes)
                .HasForeignKey(e => e.DateType);

            modelBuilder.Entity<Permissions>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("RolePermissions").MapLeftKey("Permission").MapRightKey("Role"));

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.ProjectResources)
                .WithOptional(e => e.Projects)
                .HasForeignKey(e => e.Project)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.ProjectTasks)
                .WithOptional(e => e.Projects)
                .HasForeignKey(e => e.Project)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Projects>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Projects)
                .Map(m => m.ToTable("ProjectUsers").MapLeftKey("Project").MapRightKey("User"));

            modelBuilder.Entity<Resources>()
                .HasOptional(e => e.MaterialResoucres)
                .WithRequired(e => e.Resources)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Resources>()
                .HasMany(e => e.ProjectResources)
                .WithOptional(e => e.Resources)
                .HasForeignKey(e => e.Resource)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Resources>()
                .HasOptional(e => e.WorkingResources)
                .WithRequired(e => e.Resources)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalaryTypes>()
                .HasMany(e => e.WorkingResources)
                .WithOptional(e => e.SalaryTypes)
                .HasForeignKey(e => e.SalaryType);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.ProjectTasks)
                .WithOptional(e => e.Tasks)
                .HasForeignKey(e => e.Task)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskConnections)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.Parent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskConnections1)
                .WithRequired(e => e.Tasks1)
                .HasForeignKey(e => e.Child)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskGroups)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.Parent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.TaskGroups1)
                .WithRequired(e => e.Tasks1)
                .HasForeignKey(e => e.Child)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskTypes>()
                .HasMany(e => e.TaskConnections)
                .WithRequired(e => e.TaskTypes)
                .HasForeignKey(e => e.TaskType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Persons)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WeekDays>()
                .HasMany(e => e.WorkingCalendarWeekends)
                .WithOptional(e => e.WeekDays)
                .HasForeignKey(e => e.WeekDay)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WorkingCalendars>()
                .HasMany(e => e.WorkingCalendarWeekends)
                .WithOptional(e => e.WorkingCalendars)
                .HasForeignKey(e => e.WorkingCalendar)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WorkingCalendars>()
                .HasMany(e => e.WorkingResources)
                .WithOptional(e => e.WorkingCalendars)
                .HasForeignKey(e => e.WorkingCalendar);
        }
    }
}
