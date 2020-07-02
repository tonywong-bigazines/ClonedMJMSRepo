using Mahjong.Actions.Dto;
using Mahjong.PlayHistories.Dto;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Cards.Dto
{
    public class LiquidationResultDto
    {
        public int Win { get; set; }
        public int Lose { get; set; }
        public decimal Total { get; set; }
        public List<PlayHistoryDto> Histories { get; set; }

        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
    }
}
