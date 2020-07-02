using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;
using Mahjong.PlayHistories.Dto;

namespace Mahjong.Actions.Dto
{
    public class ActionMapProfile : Profile
    {
        public ActionMapProfile()
        {
            CreateMap<PlayHistoryDto, PlayHistory>();
            CreateMap<PlayHistory, PlayHistoryDto>();

            CreateMap<PlayHistoryDetailDto, PlayHistoryDetail>();
            CreateMap<PlayHistoryDetail, PlayHistoryDetailDto>();

            CreateMap<PlayHistoryDetailPlayerDto, PlayHistoryDetailPlayer>();
            CreateMap<PlayHistoryDetailPlayer, PlayHistoryDetailPlayerDto>();            
        }
    }
}
