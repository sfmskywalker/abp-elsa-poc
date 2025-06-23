using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElsaWorkflows.UI.Extensions;

public static class UrlHelperExtensions
{
    public static string ToAbsoluteAction(
        this IUrlHelper url,
        string actionName,
        string controllerName,
        object? routeValues = null)
    {
        return url.Action(actionName, controllerName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
    }

    public static string GetBaseUrl(this IUrlHelper url)
    {
        HttpRequest request = url.ActionContext.HttpContext.Request;
        return $"{request.Scheme}://{request.Host.ToUriComponent()}";
    }

    public static string ToAbsoluteUrl(this IUrlHelper url, string virtualPath)
    {
        return url.GetBaseUrl() + url.Content(virtualPath);
    }
}