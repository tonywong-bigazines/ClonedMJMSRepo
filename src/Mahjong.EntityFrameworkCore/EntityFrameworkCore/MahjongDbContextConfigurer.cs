using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Mahjong.EntityFrameworkCore
{
    public static class MahjongDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MahjongDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MahjongDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
