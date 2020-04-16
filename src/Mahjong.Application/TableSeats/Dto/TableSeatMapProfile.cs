using AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.TableSeats.Dto
{
    public class TableSeatMapProfile : Profile
    {
        public TableSeatMapProfile()
        {
            CreateMap<TableSeatDto, TableSeat>();

            CreateMap<CreateTableSeatDto, TableSeat>();
        }
    }
}
