using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.PlayHistories.Dto
{
    public class PlayHistoryMapProfile : Profile
    {
        public PlayHistoryMapProfile()
        {
            CreateMap<PlayHistoryDto, PlayHistory>();

            CreateMap<CreatePlayHistoryDto, PlayHistory>();
        }
    }
}
