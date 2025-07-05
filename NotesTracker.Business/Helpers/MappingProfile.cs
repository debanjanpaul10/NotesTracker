// *********************************************************************************
//	<copyright file="MappingProfile.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Mapping Profile.</summary>
// *********************************************************************************

namespace NotesTracker.Business.Helpers
{
	using AutoMapper;
	using NotesTracker.Data.Entities;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Mapping Profile.
	/// </summary>
	/// <seealso cref="Profile"/>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of <see cref="MappingProfile"/>
		/// </summary>
		public MappingProfile()
		{
			CreateMap<BugReportDTO, BugReport>()
				.ForMember(dest => dest.BugId, opt => opt.Ignore())
				.ForMember(dest => dest.LoggedByUserName, opt => opt.Ignore())
				.ForMember(dest => dest.BugStatus, opt => opt.Ignore())
				.ForMember(dest => dest.BugTitle, opt => opt.MapFrom(src => src.BugTitle))
				.ForMember(dest => dest.BugDescription, opt => opt.MapFrom(src => src.BugDescription))
				.ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.PageUrl))
				.ForMember(dest => dest.BugSeverity, opt => opt.MapFrom(src => src.BugSeverity));
		}
	}
}
