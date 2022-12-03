using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HIS;

[Dependency(ReplaceServices = true)]
public class HISBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HIS";
}
