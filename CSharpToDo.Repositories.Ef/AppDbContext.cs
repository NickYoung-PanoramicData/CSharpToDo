using CSharpToDo.Repositories.Ef.Interfaces;
using CSharpToDo.Repositories.Ef.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpToDo.Repositories.Ef
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{

		}
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}

		public DbSet<ToDoModel> ToDos { get; set; } = null!;
		public DbSet<ReminderModel> Reminders { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDoModel>()
				.Property(t => t.Name)
				.HasMaxLength(250);

			modelBuilder.Entity<ReminderModel>()
				.Property(r => r.Name)
				.HasMaxLength(250);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdateTimestamps();
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}


		private void UpdateTimestamps()
		{
			var now = DateTime.UtcNow;
			foreach (var changedEntity in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
			{
				var auditableEntity = changedEntity.Entity as IAuditable;
				if (auditableEntity is not null)
				{
					switch (changedEntity.State)
					{
						case EntityState.Added:
							auditableEntity.CreatedUtc = DateTime.UtcNow;
							auditableEntity.LastModifiedUtc = null;
							break;
						case EntityState.Modified:
							changedEntity.Property(nameof(auditableEntity.CreatedUtc)).IsModified = false;
							auditableEntity.LastModifiedUtc = DateTime.UtcNow;
							break;
					}
				}
			}
		}
	}


}
