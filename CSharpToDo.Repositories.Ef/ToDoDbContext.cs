using CSharpToDo.Repositories.Ef.Interfaces;
using CSharpToDo.Repositories.Ef.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToDo.Repositories.Ef
{
	public class ToDoDbContext : DbContext
	{
		public ToDoDbContext(DbContextOptions<ToDoDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}

		public DbSet<ToDoModel> ToDos { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ToDoModel>()
			.Property(t => t.Name)
			.HasMaxLength(250);

		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdateTimestamps();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void UpdateTimestamps()
		{
			var now = DateTime.UtcNow;
			foreach (var changedEntity in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
			{
				if (changedEntity is IAuditable auditableEntity)
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
