﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class PlayHistory:Entity, IHasCreationTime
    {
        public int TableId { get; set; }
        public Table Table { get; set; }

        public int Round { get; set; }

        public Guid PlayingKey { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public PlayHistory()
        {
            CreationTime = DateTime.Now;
        }

    }
}
