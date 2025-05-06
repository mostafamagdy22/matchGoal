using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MatchGoalAPI.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
		{
			
		}
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Match> Matches { get; set; }
		public DbSet<Competition> Competitions { get; set; }
		public DbSet<PlayList> PlayLists { get; set; }
		public DbSet<PlayListMatches> PlayListMatches { get; set; }
		public DbSet<Team> Teams { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Team>().Property(t => t.ID).ValueGeneratedOnAdd();
			builder.Entity<Competition>().Property(c => c.ID).ValueGeneratedOnAdd();
			builder.Entity<Season>().Property(s => s.Id).ValueGeneratedOnAdd();
			builder.Entity<Match>().Property(m => m.ID).ValueGeneratedOnAdd();

			builder.Entity<Competition>()
				.HasOne(c => c.CurrentSeason)
				.WithMany()
				.HasForeignKey(c => c.CurrentSeasonId);

			builder.Entity<Competition>()
				.HasMany(c => c.Seasons)
				.WithOne()
				.HasForeignKey(s => s.CompetitionId);

			builder.Entity<PlayListMatches>()
				.HasKey(pm => new { pm.PlayListID, pm.MatchID });

			builder.Entity<Match>()
		.HasOne(m => m.HomeTeam)
		.WithMany(t => t.HomeMatches)
		.HasForeignKey(m => m.HomeTeamID)
		.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Match>()
				.HasOne(m => m.AwayTeam)
				.WithMany(t => t.AwayMatches)
				.HasForeignKey(m => m.AwayTeamID)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Match>()
				.HasOne(m => m.Competition)
				.WithMany(c => c.Matches)
				.HasForeignKey(m => m.CompetitionID)
				.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Match>()
				.HasCheckConstraint("CK_DifferentTeams", "HomeTeamID != AwayTeamID");
			
			base.OnModelCreating(builder);
		}
	}
}
