using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Mahjong.Authorization;

namespace Mahjong
{
    [DependsOn(
        typeof(MahjongCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MahjongApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MahjongAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MahjongApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
