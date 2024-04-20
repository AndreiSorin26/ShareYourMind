using AutoMapper;
using Services.DTOs.EntityMappings;
using Database.Entities;

namespace Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<FriendRequest, FriendRequestDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.SenderDisplayName, opt => opt.MapFrom(src => src.Sender.DisplayName))
                .ForMember(dest => dest.ReceiverDisplayName, opt => opt.MapFrom(src => src.Receiver.DisplayName));

            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.PosterDisplayName, opt => opt.MapFrom(src => src.Poster.DisplayName))
                .ForMember(dest => dest.LoveReactionCount, opt => opt.MapFrom(src => src.LoveReactionUsers.Count()))
                .ForMember(dest => dest.LaughReactionCount, opt => opt.MapFrom(src => src.LaughReactionUsers.Count()))
                .ForMember(dest => dest.DislikeReactionCount, opt => opt.MapFrom(src => src.DislikeReactionUsers.Count()));

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.PosterDisplayName, opt => opt.MapFrom(src => src.Poster.DisplayName));

            CreateMap<Report, ReportDTO>()
                .ForMember(dest => dest.ReportingUserDisplayName, opt => opt.MapFrom(src => src.Reporter.DisplayName));

            CreateMap<Feedback, FeedbackDTO>()
                .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.User.DisplayName));
        }
    }
}
