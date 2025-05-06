using AutoMapper;

namespace MatchGoalAPI.Mapping
{
	public class MatchProfile : Profile
	{
		public MatchProfile()
		{
			CreateMap<Match, MatchDto>()
				.ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Title))
				.ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Title))
				.ForMember(dest => dest.CompetetionName, opt => opt.MapFrom(src => src.Competition.Title))
				.ForMember(dest => dest.HomeTeamScore,opt => opt.MapFrom(src => src.FirstTeamScore))
				.ForMember(dest=> dest.AwayTeamScore,opt => opt.MapFrom(src => src.SecondTeamScore))
				.ReverseMap();

			CreateMap<Match, AddMatchDto>()
				.ForMember(dest => dest.HomeTeamScore, opt => opt.MapFrom(src => src.FirstTeamScore))
				.ForMember(dest => dest.AwayTeamScore, opt => opt.MapFrom(src => src.SecondTeamScore))
				.ForMember(dest => dest.Status,opt => opt.MapFrom(src => src.Status))
				.ForMember(dest => dest.HomeTeamID, opt => opt.MapFrom(src => src.HomeTeamID))
				.ForMember(dest => dest.AwayTeamID, opt => opt.MapFrom(src => src.AwayTeamID))
				.ForMember(dest => dest.CompetitionID, opt => opt.MapFrom(src => src.CompetitionID))
				.ForMember(dest => dest.WinnerTeamID, opt => opt.MapFrom(src => src.WinnerID))
				.ForMember(dest => dest.Stadium, opt => opt.MapFrom(src => src.Stadium))
				.ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src => src.MatchDate))
				.ReverseMap();
		}
	}
}
