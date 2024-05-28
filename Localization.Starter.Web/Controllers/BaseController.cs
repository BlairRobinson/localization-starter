using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace Localization.Starter.Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? culture = context.HttpContext.Request.Query["culture"];

            if (!String.IsNullOrEmpty(culture))
            {
                SetLanguageCookie(culture);
            }

            base.OnActionExecuting(context);
        }

        public void SetLanguageCookie(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
        }

        public CultureInfo GetRequestCulture()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            return rqf!.RequestCulture.Culture;
        }

        public bool IsWelsh()
        {
            return GetRequestCulture().Name == "cy-GB";
        }
    }
}
