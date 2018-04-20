using System;
using System.Threading;
using System.Threading.Tasks;
using beers_api.Data;
using Microsoft.EntityFrameworkCore;

namespace beersapi.Data
{
	public class BeersContext : DbContext
	{
		public BeersContext(DbContextOptions options) : base(options)
		{
			
		}

		public DbSet<BeerEntity> Beers { get; set; }

		public DbSet<BreweryEntity> Breweries { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<BeerEntity>().ToTable("tbl_beers"); Cuando la tabla ya existe
			//modelBuilder.Entity<BeerEntity>().Property(b => b.Name).HasColumnName("Descripcion");
			//modelBuilder.Entity<BeerEntity>().Ignore(x => x.Name);
			modelBuilder.Entity<BeerEntity>().Property<DateTime>("LastUpdatedDate");
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			var entries = this.ChangeTracker.Entries<BeerEntity>();
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
				{
					entry.Property("LastUpdatedDate").CurrentValue = DateTime.UtcNow;
				}
			}

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
