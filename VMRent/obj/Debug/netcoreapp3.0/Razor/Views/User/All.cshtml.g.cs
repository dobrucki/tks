#pragma checksum "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f77645d78e33c1c6d3302e7739b932e19aa525f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_All), @"mvc.1.0.view", @"/Views/User/All.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using System.Collections.Immutable;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using System.Text;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using Microsoft.AspNetCore.Mvc.Rendering;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
using VMRent.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f77645d78e33c1c6d3302e7739b932e19aa525f", @"/Views/User/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c66e443ba81bfd444e2b1c1ae94c4deedf2b8d44", @"/Views/_ViewImports.cshtml")]
    public class Views_User_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VMRent.ViewModels.ListUserViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 11 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
  
    ViewBag.Title = "Users";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container mt-2"">
    <h2>Users</h2>
    <table class=""table table-hover"">
        <thead>
        <th>Username</th>
        <th>Email</th>
        <th>Phone number</th>
        <th>Access level (roles)</th>
        <th></th>
        </thead>
        <tbody>
");
#nullable restore
#line 26 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
         foreach (var user in Model.Users)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>");
#nullable restore
#line 29 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(user.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 30 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(user.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 31 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(user.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>\n");
#nullable restore
#line 33 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
                      
                        string rolesNames;
                        var roles = UserManager.GetRolesAsync(user).Result;
                        rolesNames = roles.Any() ? roles.ToImmutableSortedSet().Aggregate((i, j) => i + ", " + j): string.Empty;
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
#nullable restore
#line 38 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(rolesNames);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                \n                <td>\n                    ");
#nullable restore
#line 42 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(Html.ActionLink("Details", "Details", "User", new {id = user.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                    ");
#nullable restore
#line 43 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(Html.ActionLink("Add role", "AddToRole", "User", new {id = user.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                    ");
#nullable restore
#line 44 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(Html.ActionLink("Remove role", "RemoveFromRole", "User", new {id = user.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                    ");
#nullable restore
#line 45 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
               Write(Html.ActionLink("Edit", "Edit", "User", new {id = user.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 48 "/Users/dobrucki/dev/VMRent/VMRent/Views/User/All.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\n    </table>\n</div>\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public RoleManager<Role> RoleManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<User> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VMRent.ViewModels.ListUserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591