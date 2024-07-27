using Microsoft.AspNetCore.Localization;

namespace Study_ASPNET_Core.Language;

public class Lan
{
    public static void ChangeLanguage(HttpContext context, string?culture)
    {
        culture ??= "ko";

        var option = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMonths(1),
            IsEssential = true
        };

        context.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            option
        );
    }
}