using HIS.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HIS.Blazor;

public abstract class HISComponentBase : AbpComponentBase
{
    protected HISComponentBase()
    {
        LocalizationResource = typeof(HISResource);
    }
}
