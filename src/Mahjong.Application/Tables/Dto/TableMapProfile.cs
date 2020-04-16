using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.Tables.Dto
{
    public class TableMapProfile : Profile
    {
        public TableMapProfile()
        {
            CreateMap<TableDto, Table>();

            CreateMap<CreateTableDto, Table>();
        }
    }
}
