using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Tables.Dto
{
    public class TableInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal Commission1 { get; set; }
        public decimal Commission2 { get; set; }
        public decimal Commission3 { get; set; }
        public decimal Commission4 { get; set; }
        public decimal Commission5 { get; set; }
        public decimal Commission6 { get; set; }
        public decimal Commission7 { get; set; }
        public decimal Commission8 { get; set; }
        public decimal Commission9 { get; set; }
    }
}
