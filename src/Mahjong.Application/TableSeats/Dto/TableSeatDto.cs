using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Mahjong.Authorization.Users;
using Mahjong.Cards.Dto;
using Mahjong.Mahjong;

namespace Mahjong.TableSeats.Dto
{
    [AutoMapFrom(typeof(TableSeat))]
    public class TableSeatDto : EntityDto<int>
    {
        public string Position { get; set; }

        public string DeviceConnectionId { get; set; }

        public string PlayerType { get; set; }

        public string PlayerCardId { get; set; }

        public CardDto PlayerCard { get; set; }

        public string StaffCardId { get; set; }
        public CardDto StaffCard { get; set; }

        public int Round { get; set; }

        public decimal HelpPlayAmount { get; set; }

    }
}
