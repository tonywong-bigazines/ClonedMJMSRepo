using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Mahjong.EntityFrameworkCore;
using Mahjong.Tables;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.SignalRService
{
    public class RecordHub:Hub, ITransientDependency
    {
        private readonly TableAppService _tableAppService;

        public IAbpSession AbpSession { get; set; }

        public ILogger Logger { get; set; }

        public RecordHub( TableAppService tableAppService)
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            _tableAppService = tableAppService;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("getMessage", string.Format("User {0}: {1}", AbpSession.UserId, message));
        }

        public void RegisterDevice(int tableId,string position)
        {
            _tableAppService.RegisterDevice(tableId, position, Context.ConnectionId);  
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Logger.Debug("A client connected to MyChatHub: " + Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            Logger.Debug("A client disconnected from MyChatHub: " + Context.ConnectionId);
        }
    }
}
