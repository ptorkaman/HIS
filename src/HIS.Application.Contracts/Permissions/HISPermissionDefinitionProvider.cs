using HIS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HIS.Permissions;

public class HISPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HISPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HISPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HISResource>(name);
    }
}
