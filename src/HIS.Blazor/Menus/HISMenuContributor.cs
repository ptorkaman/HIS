using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HIS.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace HIS.Blazor.Menus;

public class HISMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public HISMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<HISResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HISMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        context.Menu.AddItem(
    new ApplicationMenuItem(
        "LookUp",
        "LookUp",
        icon: "fa fa-book"
    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Authors",
            "Authors",
            url: "/authors"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.AdministrativeSexes",
            "AdministrativeSexes",
            url: "/AdministrativeSexes"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.AdministrativeGenders",
            "AdministrativeGenders",
            url: "/AdministrativeGenders"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.ContactModes",
            "ContactModes",
            url: "/ContactModes"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Countries",
            "Countries",
            url: "/Countries"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Educations",
            "Educations",
            url: "/Educations"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Facilities",
            "Facilities",
            url: "/Facilities"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.IdentifierTypes",
            "IdentifierTypes",
            url: "/IdentifierTypes"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.InActiveReasons",
            "InActiveReasons",
            url: "/InActiveReasons"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Languages",
            "Languages",
            url: "/Languages"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.MaritalStatuses",
            "MaritalStatuses",
            url: "/MaritalStatuses"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Nationalities",
            "Nationalities",
            url: "/Nationalities"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Races",
            "Races",
            url: "/Races"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Relationships",
            "Relationships",
            url: "/Relationships"
        )

    ).AddItem(
        new ApplicationMenuItem(
            "LookUp.Religions",
            "Religions",
            url: "/Religions"
        )

    )
);

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
