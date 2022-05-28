using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class FilterJmbgValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                Regex r = new Regex("^[0-9]+$");
                if (!r.IsMatch(text) && text.Length > 0)
                {
                    return new ValidationResult(false, "You must enter only digits!");

                }
                if (text.Length > 0 && text.Length != 13)
                {
                    return new ValidationResult(false, "Length must be 13!");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }

        }
    }
}
