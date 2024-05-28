using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Localization.Starter.Web.ViewModels
{
    public class FormExampleViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Select a title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter your first name")]
        public string FirstName { get; set; } = string.Empty;

        public string? AreYouUnder25 { get; set; }

        public string? AmountApplyingFor { get; set; } = string.Empty;

        [Range(typeof(bool), "true", "true", ErrorMessage = "AuthorisedToSubmitRequired")]
        [Display(Name = "AuthorisedToSubmitLabel")]
        public bool AuthorisedToSubmit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var stringLocalizer =
                validationContext.GetService(typeof(IStringLocalizer<FormExampleViewModel>)) as
                    IStringLocalizer<FormExampleViewModel>;

            var declarationsChecked = DeclarationCompleted(AreYouUnder25);

            if (!declarationsChecked)
            {
                yield return new ValidationResult(stringLocalizer?["Must select and option"].Value,
                    new[] { "AreYouUnder25" });
            }
        }

        private bool DeclarationCompleted(string? declarationValue)
        {
            var declarationString = declarationValue ?? "";
            return declarationString.Equals("yes") || declarationString.Equals("no");
        }
    }
}
