namespace MatchGoalAPI.MockData
{
	public static class MockData
	{
		public static List<Team> Teams = new List<Team>
		{
			new Team { ID = -1, Founded = 1600, Title = "Manchester United", ShortName = "MU", Country = "England", ClubColors = "Red / White" },
			new Team { ID = -2, Founded = 1900, Title = "Manchester City", ShortName = "MC", Country = "England", ClubColors = "Blue / White" },
			new Team { ID = -3, Founded = 1740, Title = "Liverpool", ShortName = "LIV", Country = "England", ClubColors = "Red / White" },
			new Team { ID = -4, Founded = 1743, Title = "Chelsea", ShortName = "CHE", Country = "England", ClubColors = "Blue / White" },
			new Team { ID = -5, Founded = 1940, Title = "Arsenal", ShortName = "ARS", Country = "England", ClubColors = "Red / White" },
			new Team { ID = -6, Founded = 1960, Title = "Tottenham Hotspur", ShortName = "TH", Country = "England", ClubColors = "White / Blue" },
			new Team { ID = -7, Founded = 1944, Title = "Barcelona", ShortName = "BAR", Country = "Spain", ClubColors = "Blue / Red" },
			new Team { ID = -8, Founded = 1920, Title = "Real Madrid", ShortName = "RM", Country = "Spain", ClubColors = "White / Blue" },
			new Team { ID = -9, Founded = 1890, Title = "Atletico Madrid", ShortName = "ATM", Country = "Spain", ClubColors = "Red / White" },
			new Team { ID = -10, Founded = 1980, Title = "Valencia", ShortName = "VAL", Country = "Spain", ClubColors = "Orange / White" },
			new Team { ID = -11, Founded = 1600, Title = "Sevilla", ShortName = "SEV", Country = "Spain", ClubColors = "Red / White" },
			new Team { ID = -12, Founded = 1700, Title = "Borussia Dortmund", ShortName = "BVB", Country = "Germany", ClubColors = "Yellow / Black" },
			new Team { ID = -13, Founded = 1900, Title = "Bayern Munich", ShortName = "BAY", Country = "Germany", ClubColors = "Red / White" },
			new Team { ID = -14, Founded = 1900, Title = "RB Leipzig", ShortName = "RBL", Country = "Germany", ClubColors = "Red / White" },
			new Team { ID = -15, Founded = 1800, Title = "Inter Milan", ShortName = "INT", Country = "Italy", ClubColors = "Blue / Black" }
		};

		public static List<Competition> Competitions = new List<Competition>
		{
			new Competition
			{
				ID = -1,
				Title = "Premier League",
				Code = "PL",
				Type = "League",
				CurrentSeasonId = -1,
				CurrentSeason = new Season { Id = -1, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -1 },
				Seasons = new List<Season>
				{
					new Season { Id = -2, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -1 }
				}
			},
			new Competition
			{
				ID = -2,
				Title = "La Liga",
				Code = "LL",
				Type = "League",
				CurrentSeasonId = -3,
				CurrentSeason = new Season { Id = -3, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -2 },
				Seasons = new List<Season>
				{
					new Season { Id = -4, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -2 }
				}
			},
			new Competition
			{
				ID = -3,
				Title = "Bundesliga",
				Code = "BL",
				Type = "League",
				CurrentSeasonId = -5,
				CurrentSeason = new Season { Id = -5, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -3 },
				Seasons = new List<Season>
				{
					new Season { Id = -6, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -3 }
				}
			},
			new Competition
			{
				ID = -4,
				Title = "Serie A",
				Code = "SA",
				Type = "League",
				CurrentSeasonId = -7,
				CurrentSeason = new Season { Id = -7, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -4 },
				Seasons = new List<Season>
				{
					new Season { Id = -8, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -4 }
				}
			},
			new Competition
			{
				ID = -5,
				Title = "Ligue 1",
				Code = "L1",
				Type = "League",
				CurrentSeasonId = -9,
				CurrentSeason = new Season { Id = -9, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -5 },
				Seasons = new List<Season>
				{
					new Season { Id = -10, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -5 }
				}
			},
			new Competition
			{
				ID = -6,
				Title = "Eredivisie",
				Code = "ED",
				Type = "League",
				CurrentSeasonId = -11,
				CurrentSeason = new Season { Id = -11, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -6 },
				Seasons = new List<Season>
				{
					new Season { Id = -12, StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(30), CurrentMatchDay = 1, Winner = null, CompetitionId = -6 }
				}
			}
		};

		public static List<Match> Matches = new List<Match>
		{
			new Match
			{
				ID = -1,
				HomeTeamID = -1,
				AwayTeamID = -2,
				MatchDate = DateTime.Now.AddDays(1),
				Status = MatchStatusEnum.NotStarted,
				FirstTeamScore = null,
				SecondTeamScore = null,
				CompetitionID = -1,
				LastScoreUpdate = null
			},
			new Match
			{
				ID = -2,
				HomeTeamID = -3,
				AwayTeamID = -4,
				MatchDate = DateTime.Now.AddDays(2),
				Status = MatchStatusEnum.NotStarted,
				FirstTeamScore = null,
				SecondTeamScore = null,
				CompetitionID = -1,
				LastScoreUpdate = null
			},
			new Match
			{
				ID = -3,
				HomeTeamID = -5,
				AwayTeamID = -6,
				MatchDate = DateTime.Now.AddDays(3),
				Status = MatchStatusEnum.NotStarted,
				FirstTeamScore = null,
				SecondTeamScore = null,
				CompetitionID = -1,
				LastScoreUpdate = null
			},
			new Match
			{
				ID = -4,
				HomeTeamID = -7,
				AwayTeamID = -8,
				MatchDate = DateTime.Now.AddDays(4),
				Status = MatchStatusEnum.NotStarted,
				FirstTeamScore = null,
				SecondTeamScore = null,
				CompetitionID = -2,
				LastScoreUpdate = null
			}
		};
	}
}