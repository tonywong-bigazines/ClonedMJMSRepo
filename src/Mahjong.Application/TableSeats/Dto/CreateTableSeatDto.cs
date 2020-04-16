using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Mahjong.Authorization.Users;
using Mahjong.Mahjong;

namespace Mahjong.TableSeats.Dto
{
    [AutoMapTo(typeof(TableSeat))]
    public class CreateTableSeatDto
    {
        public int TableId { get; set; }

        public string Position { get; set; }

        public string TableCardId { get; set; }

        public string PlayerCardId { get; set; }

        public string PlayerType { get; set; }
    }
}
