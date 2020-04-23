using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using Mahjong.Mahjong;
using Mahjong.TableSeats.Dto;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mahjong.SignalRService
{
    public class TestAppService: AbpServiceBase,IApplicationService
    {
        private readonly IHubContext<RecordHub> _hubContext;
        public TestAppService(IHubContext<RecordHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void SendMessage()
        {
            await _hubContext.Clients.All.SendAsync("getMessage", $"msg from server:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
