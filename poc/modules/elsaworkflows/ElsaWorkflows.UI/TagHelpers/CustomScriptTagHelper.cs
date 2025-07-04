using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling.TagHelpers;

namespace ElsaWorkflows.UI.TagHelpers;

[HtmlTargetElement("blazor-script", TagStructure = TagStructure.NormalOrSelfClosing)]
public class BlazorScriptTagHelper : AbpBundleItemTagHelper<BlazorScriptTagHelper, BlazorScriptTagHelperService>, IBundleItemTagHelper
{
    [HtmlAttributeName("defer")]
    public bool Defer { get; set; }
    
    [HtmlAttributeName("autostart")]
    public bool AutoStart { get; set; }

    public BlazorScriptTagHelper(BlazorScriptTagHelperService service)
        : base(service)
    {

    }

    protected override string GetFileExtension()
    {
        return "js";
    }
}

public class BlazorScriptTagHelperService : AbpBundleItemTagHelperService<BlazorScriptTagHelper, BlazorScriptTagHelperService>
{
    public BlazorScriptTagHelperService(BlazorTagHelperScriptService resourceService)
        : base(resourceService)
    {
    }
}

public class BlazorTagHelperScriptService : AbpTagHelperResourceService
{
    public BlazorTagHelperScriptService(
        IBundleManager bundleManager,
        IOptions<AbpBundlingOptions> options,
        IWebHostEnvironment hostingEnvironment) : base(
            bundleManager,
            options,
            hostingEnvironment)
    {
    }

    protected override void CreateBundle(string bundleName, List<BundleTagHelperItem> bundleItems)
    {
        Options.ScriptBundles.TryAdd(
            bundleName,
            configuration => bundleItems.ForEach(bi => bi.AddToConfiguration(configuration)),
            HostingEnvironment.IsDevelopment() && bundleItems.Any()
        );
    }

    protected override async Task<IReadOnlyList<BundleFile>> GetBundleFilesAsync(string bundleName)
    {
        return await BundleManager.GetScriptBundleFilesAsync(bundleName);
    }

    protected override void AddHtmlTag(ViewContext viewContext, TagHelper tagHelper, TagHelperContext context, TagHelperOutput output, BundleFile file, IFileInfo? fileInfo = null)
    {
        var defer = tagHelper switch
        {
            AbpScriptTagHelper scriptTagHelper => scriptTagHelper.Defer,
            AbpScriptBundleTagHelper scriptBundleTagHelper => scriptBundleTagHelper.Defer,
            _ => false
        };
        
        var autostart = tagHelper switch
        {
            BlazorScriptTagHelper scriptTagHelper => scriptTagHelper.AutoStart,
            _ => false
        };

        var deferText = (defer || Options.DeferScriptsByDefault || Options.DeferScripts.Any(x => file.FileName.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                ? "defer "
                : string.Empty;
        
        var autostartText = autostart ? "autostart=\"true\" " : string.Empty;
        
        var nonceText = (viewContext.HttpContext.Items.TryGetValue(AbpAspNetCoreConsts.ScriptNonceKey, out var nonce) && nonce is string nonceString && !string.IsNullOrEmpty(nonceString))
            ? $"nonce=\"{nonceString}\" "
            : string.Empty;
        var src = file.IsExternalFile ? file.FileName : viewContext.GetUrlHelper().Content((file.FileName + "?_v=" + fileInfo!.LastModified.UtcTicks).EnsureStartsWith('~'));
        output.Content.AppendHtml($"<script {deferText}{nonceText}{autostartText}src=\"{src}\"></script>{Environment.NewLine}");
    }
}