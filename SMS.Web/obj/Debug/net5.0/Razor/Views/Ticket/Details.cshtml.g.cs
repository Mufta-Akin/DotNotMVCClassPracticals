#pragma checksum "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7eff92d11f7365c3911667bb6a7a3845c94b014d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ticket_Details), @"mvc.1.0.view", @"/Views/Ticket/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\_ViewImports.cshtml"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\_ViewImports.cshtml"
using SMS.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\_ViewImports.cshtml"
using SMS.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\_ViewImports.cshtml"
using SMS.Data.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7eff92d11f7365c3911667bb6a7a3845c94b014d", @"/Views/Ticket/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82d9e1f2107faec1f93130aaf76fb1213cbef0c7", @"/Views/_ViewImports.cshtml")]
    public class Views_Ticket_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Ticket>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Breadcrumbs", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("modal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-target", new global::Microsoft.AspNetCore.Html.HtmlString("#closeModal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CloseModal", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::SMS.Web.TagHelpers.ConditionTagHelper __SMS_Web_TagHelpers_ConditionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<!-- Breadcrumbs using partial view -->\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "7eff92d11f7365c3911667bb6a7a3845c94b014d5998", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 4 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = (new [] { ("/", "Home"), ("/ticket", "Tickets"), ("","Close") });

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    \n<div class=\"col card rounded shadow-lg p-4\">\n\n    <h3 class=\"mb-4\">Ticket</h3>\n\n    <dl class=\"row\">       \n        <dt class=\"col-4\">Id</dt>\n        <dd class=\"col-8\">");
#nullable restore
#line 12 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
                     Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n\n        <dt class=\"col-4\">Created On</dt>\n        <dd class=\"col-8\">");
#nullable restore
#line 15 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
                     Write(Model.CreatedOn);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n     \n        <dt class=\"col-4\">Issue</dt>\n        <dd class=\"col-8\">");
#nullable restore
#line 18 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
                     Write(Model.Issue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n         \n        <dt class=\"col-4\">Created By</dt>\n        <dd class=\"col-8\">");
#nullable restore
#line 21 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
                     Write(Model.Student.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n   \n        <dt class=\"col-4\">Resolved On</dt>\n        <dd class=\"col-8\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7eff92d11f7365c3911667bb6a7a3845c94b014d9106", async() => {
                WriteLiteral(" \n                Unresolved\n            ");
            }
            );
            __SMS_Web_TagHelpers_ConditionTagHelper = CreateTagHelper<global::SMS.Web.TagHelpers.ConditionTagHelper>();
            __tagHelperExecutionContext.Add(__SMS_Web_TagHelpers_ConditionTagHelper);
#nullable restore
#line 25 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
__SMS_Web_TagHelpers_ConditionTagHelper.Condition = (Model.Active);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-condition", __SMS_Web_TagHelpers_ConditionTagHelper.Condition, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7eff92d11f7365c3911667bb6a7a3845c94b014d10512", async() => {
                WriteLiteral(" \n                ");
#nullable restore
#line 29 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
           Write(Model.ResolvedOn);

#line default
#line hidden
#nullable disable
                WriteLiteral("\n            ");
            }
            );
            __SMS_Web_TagHelpers_ConditionTagHelper = CreateTagHelper<global::SMS.Web.TagHelpers.ConditionTagHelper>();
            __tagHelperExecutionContext.Add(__SMS_Web_TagHelpers_ConditionTagHelper);
#nullable restore
#line 28 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
__SMS_Web_TagHelpers_ConditionTagHelper.Condition = (!Model.Active);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-condition", __SMS_Web_TagHelpers_ConditionTagHelper.Condition, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </dd>\n     \n        <dt class=\"col-4\">Resolution</dt>\n        <dd class=\"col-8\">");
#nullable restore
#line 34 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
                     Write(Model.Resolution);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n      \n    </dl>\n\n    <div class=\"mt-4\">           \n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7eff92d11f7365c3911667bb6a7a3845c94b014d12536", async() => {
                WriteLiteral("\n            <i class=\"bi bi-trash mr-1\"></i>Close\n        ");
            }
            );
            __SMS_Web_TagHelpers_ConditionTagHelper = CreateTagHelper<global::SMS.Web.TagHelpers.ConditionTagHelper>();
            __tagHelperExecutionContext.Add(__SMS_Web_TagHelpers_ConditionTagHelper);
#nullable restore
#line 39 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
__SMS_Web_TagHelpers_ConditionTagHelper.Condition = (User.HasOneOfRoles("admin,manager"));

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-condition", __SMS_Web_TagHelpers_ConditionTagHelper.Condition, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "disabled", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 39 "C:\Users\mufta\Downloads\PracticalComplete\SMS.Web\Views\Ticket\Details.cshtml"
AddHtmlAttributeValue("", 1144, !Model.Active, 1144, 16, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7eff92d11f7365c3911667bb6a7a3845c94b014d14698", async() => {
                WriteLiteral("Cancel");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </div>\n\n</div> \n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7eff92d11f7365c3911667bb6a7a3845c94b014d15960", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Ticket> Html { get; private set; }
    }
}
#pragma warning restore 1591
