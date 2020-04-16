using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Mahjong.Authorization.Roles;
using Mahjong.Authorization.Users;
using Mahjong.MultiTenancy;
using Mahjong.Mahjong;

namespace Mahjong.EntityFrameworkCore
{
    public class MahjongDbContext : AbpZeroDbContext<Tenant, Role, User, MahjongDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public MahjongDbContext(DbContextOptions<MahjongDbContext> options)
            : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<TableSeat> TableSeats { get; set; }
        public DbSet<Card> Cards { get; set; }

        public DbSet<PlayHistory> PlayHistoreis { get; set; }
        public DbSet<PlayHistoryDetail> PlayHistoryDetails { get; set; }
    }
}
