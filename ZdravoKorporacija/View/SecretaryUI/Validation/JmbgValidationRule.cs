using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class JmbgValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (text.Length != 13)
                    return new ValidationResult(false, "This field is necessary, length must be 13!");
                Regex r = new Regex("^[0-9]+$");
                if (r.IsMatch(text))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "You must enter only digits!");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }

        }
    }
}
