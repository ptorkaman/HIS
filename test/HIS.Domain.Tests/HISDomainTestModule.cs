using HIS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HIS;

[DependsOn(
    typeof(HISEntityFrameworkCoreTestModule)
    )]
public class HISDomainTestModule : AbpModule
{

}
