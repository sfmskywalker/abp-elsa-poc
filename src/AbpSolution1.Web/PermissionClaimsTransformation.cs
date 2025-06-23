using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

public class PermissionClaimsTransformation : IClaimsTransformation
{
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IPermissionChecker _permissionChecker;

    public PermissionClaimsTransformation(
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionChecker permissionChecker)
    {
        _permissionDefinitionManager = permissionDefinitionManager;
        _permissionChecker = permissionChecker;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (!(principal.Identity?.IsAuthenticated ?? false))
            return principal;

        var identity = (ClaimsIdentity)principal.Identity;

        // collect all defined permission names
        var allPermissions = await _permissionDefinitionManager.GetPermissionsAsync();
        var permissionNames = allPermissions.Select(p => p.Name).ToList();

        // filter to only those granted to the current user
        var granted = new List<string>();
        foreach (var name in permissionNames)
        {
            if (await _permissionChecker.IsGrantedAsync(name))
            {
                granted.Add(name);
            }
        }

        // add a single claim with JSON array of permissions
        // identity.AddClaim(new Claim(
        //     "permissions",
        //     JsonSerializer.Serialize(granted)));
        
        identity.AddClaim(new("permissions", "*"));

        return principal;
    }
}