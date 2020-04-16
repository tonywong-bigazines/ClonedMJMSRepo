using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Cards.Dto
{
    public class CardMapProfile : Profile
    {
        public CardMapProfile()
        {
            CreateMap<CardDto, Card>();

            CreateMap<CreateCardDto, Card>();
        }
    }
}
