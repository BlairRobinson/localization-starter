using Localization.Starter.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Localization.Starter.Web.Controllers
{
    public class FormsController : BaseController
    {
        private readonly ILogger<FormsController> _logger;
        private readonly IStringLocalizer _localizer;

        public FormsController(ILogger<FormsController> logger, IStringLocalizer<FormsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = new FormExampleViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(FormExampleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int amountApplyingFor = getIntOrZero(model.AmountApplyingFor);
            if (amountApplyingFor > 10000)
            {
                _logger.LogWarning("User has entered more than 10,000");
                ModelState.TryAddModelError("AmountApplyingFor", _localizer["AmountAppliedForError"]);
                return View(model);
            }

            return RedirectToAction("Edit");
        }

        private int getIntOrZero(string? input)
        {
            int toSum;
            int.TryParse(input ?? "0", out toSum);
            return toSum;
        }
    }
}
