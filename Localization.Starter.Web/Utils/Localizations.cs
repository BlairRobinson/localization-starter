using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Localization.Starter.Web.Utils
{
    public static class Localizations
    {
        public static CultureInfo GetRequestCulture(HttpContext request)
        {
            var requestCultureFeature = request.Features.Get<IRequestCultureFeature>();
            return requestCultureFeature!.RequestCulture.Culture;
        }

        public static CultureInfo GetRequestUICulture(HttpContext request)
        {
            var requestCultureFeature = request.Features.Get<IRequestCultureFeature>();
            return requestCultureFeature!.RequestCulture.UICulture;
        }
    }
}
