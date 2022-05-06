using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class FirstAndLastNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (text.Length == 0)
                    return new ValidationResult(false, "This field is necessary!");

                Regex r = new Regex("^[a-zA-Z-\\s]+$");
                if (r.IsMatch(text))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Only letters, spaces and hyphens!");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured!");
            }

        }
    }
}
