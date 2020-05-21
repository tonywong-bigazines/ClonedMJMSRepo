using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.MahjongActions.Dto
{
    public class MahjongActionMapProfile : Profile
    {
        public MahjongActionMapProfile()
        {
            CreateMap<MahjongActionDto, MahJongAction>();

            CreateMap<CreateMahjongActionDto, MahJongAction>();
        }
    }
}
