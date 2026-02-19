using System.Security.Claims;
using System.Security.Principal;

/// <summary>
/// Claims extension
/// </summary>
public static class IIdentityExtension
{
    /// <summary>
    /// Obter uma variável do Claim
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <param name="claimType"></param>
    /// <returns></returns>
    public static string GetClaim(this IIdentity id, string claimType)
    {
        try
        {
            var claimsIdentity = ((ClaimsIdentity)id).FindFirst(claimType);
            if (claimsIdentity == null)
                return "";
            string value = claimsIdentity.Value;
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
            //string value = ((ClaimsIdentity)id).FindFirst(claimType)?.Value;
            //if (string.IsNullOrEmpty(value))
            //    value = string.Empty;
            //return value;
        }
        catch (System.Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// Alterar uma propriedade do claim
    /// </summary>
    /// <param name="id"></param>
    /// <param name="claimType"></param>
    /// <param name="newClaim"></param>
    /// <returns></returns>
    public static string SetClaim(this IIdentity id, string claimType, string newClaim)
    {
        try
        {
            var claimsIdentity = ((ClaimsIdentity)id).FindFirst(claimType);
            if (claimsIdentity == null)
                return "";

            string value = claimsIdentity.Value;
            value = newClaim;
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
        catch (System.Exception)
        {
            return "";
        }
    }
}