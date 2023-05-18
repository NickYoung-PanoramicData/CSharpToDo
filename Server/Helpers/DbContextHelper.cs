using CSharpToDo.Repositories.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace CSharpToDo.Server.Helpers
{
	internal static class DbContextHelper
	{
		internal static async Task EnsureDatabaseOkAsync(IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			var services = scope.ServiceProvider;
			var logger = services.GetRequiredService<ILogger<AppDbContext>>();
			using var dbContext = services.GetRequiredService<AppDbContext>();
			logger.LogInformation("Checking Database...");
			var databaseExists = dbContext.GetService<IRelationalDatabaseCreator>().Exists();
			if (databaseExists)
			{
				logger.LogInformation("Found Database");
			}

			var pendingMigrations = (await dbContext.Database.GetPendingMigrationsAsync().ConfigureAwait(false)).ToList();
			if (pendingMigrations.Count > 0)
			{
				var stopWatch = Stopwatch.StartNew();
				logger.LogInformation("Checking Database Migrations... {MigrationCount} Migrations to apply...", pendingMigrations.Count);

				var migrator = dbContext.Database.GetInfrastructure().GetRequiredService<IMigrator>();
				foreach (var migrationName in pendingMigrations)
				{
					logger.LogInformation("Applying Migration {MigrationName}", migrationName);
					await migrator.MigrateAsync(migrationName).ConfigureAwait(false);
				}

				logger.LogInformation("Database migrations complete after {DurationSeconds:N1}s", stopWatch.Elapsed.TotalSeconds);
			}
			else
			{
				logger.LogInformation("Checking database migrations... No migrations to apply");
			}
		}
	}
}
