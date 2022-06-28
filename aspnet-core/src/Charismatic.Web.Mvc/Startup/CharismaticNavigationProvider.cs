using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Charismatic.Authorization;

namespace Charismatic.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class CharismaticNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("DashBoard"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Doctors,
                        L("Doctors"),
                        url: "Doctors",
                        icon: "fas fa-home",
                        requiresAuthentication: true,
                           permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Employees,
                        L("Employees"),
                        url: "Employees",
                        icon: "fas fa-home",
                        requiresAuthentication: true,
                           permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)

                        
                    )).AddItem(
                    new MenuItemDefinition(
                        PageNames.Admins,
                        L("Admins"),
                        url: "Admins",
                        icon: "fas fa-home",
                        requiresAuthentication: true,
                           permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)


                    )).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "System Indices",
                        L("System_Index"),
                        icon: "fas fa-circle",
                         permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    ).AddItem(new MenuItemDefinition(
                            PageNames.Centers,
                            L("Centers"),
                            url: "Centers",
                            icon: "fas fa-list",
                            order: 0
                           

                            )
                    )
                    //.AddItem(new MenuItemDefinition(
                    //        PageNames.CaseTypes,
                    //        L("CaseTypes"),
                    //        url: "CaseTypes",
                    //        icon: "fas fa-list",
                    //        order: 0
                    //        //  permissionDependency: new CMMSPermissionDependency(PermissionNames.SelectionCriteria_View)
                    //        )
                    //)
                    .AddItem(new MenuItemDefinition(
                            PageNames.ProductsIndex,
                            L("Products"),
                            url: "Products",
                            icon: "fas fa-product",
                            order: 0
                            )
                    ).AddItem(new MenuItemDefinition(
                            PageNames.Specialties,
                            L("Specialties"),
                            url: "Specialties",
                            icon: "fas fa-list",
                            order: 0

                            )
                    ).AddItem(new MenuItemDefinition(
                            PageNames.Departments,
                            L("Departments"),
                            url: "Departments",
                            icon: "fas fa-list",
                            order: 0

                            )
                    ).AddItem(new MenuItemDefinition(
                            PageNames.Countries,
                            L("Countries"),
                            url: "Countries",
                            icon: "fas fa-list",
                            isVisible:false,
                            order: 0

                            ))
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "User Managment",
                        L("user_managment"),
                        icon: "fas fa-circle"
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                )
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Department Managment",
                        L("department_managment"),
                        icon: "fas fa-circle",
                //    ).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "fas fa-theater-masks",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //)
                ))
                .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        name: PageNames.Cases,
                        displayName: L("case_managment"),
                        url: "Cases",
                        icon: "fas fa-circle",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Doctors)
                //    )
                //    ).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "fas fa-users",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "fas fa-theater-masks",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //)
                )
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                          PageNames.Missions,
                       L("Missions"),
                        url: "Missions",
                        //"Tasks Managment ",
                        //L("tasks_managment"),
                        icon: "fas fa-tasks",
                //    ).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "fas fa-users",
                       permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Employees)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "fas fa-theater-masks",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //)
                )
                    )


                            .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Agenda Managment ",
                        L("agenda_managment"),
                        icon: "fas fa-circle"
                //    ).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "fas fa-users",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "fas fa-theater-masks",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //)
                )


                 ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "CasePRoduct  Managment ",
                        L("case_product_managment"),
                        icon: "fas fa-circle",
                        url: "Products"
                       //permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)

                //    ).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Users,
                //        L("Users"),
                //        url: "Users",
                //        icon: "fas fa-users",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                //    )
                //).AddItem(
                //    new MenuItemDefinition(
                //        PageNames.Roles,
                //        L("Roles"),
                //        url: "Roles",
                //        icon: "fas fa-theater-masks",
                //        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                //    )
                //)
                )
                    );



        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CharismaticConsts.LocalizationTokens);
        }
    }
}