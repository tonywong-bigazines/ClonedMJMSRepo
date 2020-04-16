using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Mahjong.Configuration;
using Mahjong.Web;

namespace Mahjong.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MahjongDbContextFactory : IDesignTimeDbContextFactory<MahjongDbContext>
    {
        public MahjongDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MahjongDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MahjongDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MahjongConsts.ConnectionStringName));

            return new MahjongDbContext(builder.Options);
        }
    }
}
