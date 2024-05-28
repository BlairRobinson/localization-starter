using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Localization.Starter.Web.Utils
{
    public static class Localizations
    {
        public static CultureInfo GetRequestCulture(HttpContext request)
        {
            var rqf = request.Features.Get<IRequestCultureFeature>();
            return rqf!.RequestCulture.Culture;
        }

        public static bool IsWelsh(HttpContext request)
        {
            return GetRequestCulture(request).Name == "cy-GB";
        }
    }
}
