using System.Threading.Tasks;
using Acme.BookLibrary.Localization;
using Acme.BookLibrary.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Acme.BookLibrary.Web.Menus;

public class BookLibraryMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BookLibraryResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BookLibraryMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.AddItem(
    new ApplicationMenuItem(
        "BooksLibrary",
        l["Menu:BooksLibrary"],
        icon: "fa fa-book"
    ).
    AddItem(
        new ApplicationMenuItem(
            "BooksLibrary.Checkouts",
            l["Menu:Checkouts"],
            url: "/Checkouts"
        )).AddItem(
        new ApplicationMenuItem(
            "BooksLibrary.Books",
            l["Menu:Books"],
            url: "/Books"
        )
    ).AddItem(
        new ApplicationMenuItem(
            "BooksLibrary.MemberCards",
            l["Menu:MemberCards"],
            url: "/MemberCards"
        )
    ).AddItem(
        new ApplicationMenuItem(
            "BooksLibrary.Authors",
            l["Menu:Authors"],
            url: "/Authors"
        )
    )
);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}