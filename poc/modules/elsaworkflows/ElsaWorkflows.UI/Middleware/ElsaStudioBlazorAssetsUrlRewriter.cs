using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ElsaWorkflows.UI.Middleware;

public class ElsaStudioBlazorAssetsUrlRewriter(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        // 1) Check if the request path itself contains /elsa/workflows/.../_content/...
        var path = context.Request.Path.Value ?? string.Empty;
        if (path.Contains("/elsa/workflows", StringComparison.OrdinalIgnoreCase)
            && path.Contains("_content/", StringComparison.OrdinalIgnoreCase))
        {
            // Example: /elsa/workflows/edit/XYZ_content/Elsa.Studio.DomInterop/dom.entry.js
            // We want to rewrite to _content/Elsa.Studio.DomInterop/dom.entry.js
            var index = path.IndexOf("_content/", StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                var resourcePath = path.Substring(index); // e.g. "_content/Elsa.Studio.DomInterop/dom.entry.js"
                var newPath = "/" + resourcePath;
                context.Request.Path = newPath;

                // Optionally clear the query string if you don't want it carried forward.
                context.Request.QueryString = QueryString.Empty;
            }
        }
        else
        {
            // 2) Otherwise, check if there's a `returnUrl` query parameter containing /_content/...
            //    e.g. "?returnUrl=/elsa/workflows/WorkflowDefinitions/Edit/XYZ/_content/..."
            if (context.Request.Query.TryGetValue("returnUrl", out var returnUrlValues))
            {
                var returnUrl = returnUrlValues.ToString();
                var index = returnUrl.IndexOf("/_content/", StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    var resourcePath = returnUrl.Substring(index); // e.g. "/_content/Microsoft.AspNetCore.Components.CustomElements/..."
                    var newPath = resourcePath;
                    context.Request.Path = newPath;

                    // Remove the query string so the static file middleware sees a clean path.
                    context.Request.QueryString = QueryString.Empty;
                }
            }
        }

        await next(context);
    }
}

public static class ElsaStudioBlazorAssetsUrlRewriterExtensions
{
    public static IApplicationBuilder UseElsaStudioBlazorAssetsUrlRewriter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ElsaStudioBlazorAssetsUrlRewriter>();
    }
}