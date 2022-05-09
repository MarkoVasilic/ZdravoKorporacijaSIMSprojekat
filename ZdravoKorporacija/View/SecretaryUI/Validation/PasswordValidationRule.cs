using System.Globalization;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.SecretaryUI.Validation
{
    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (text.Length == 0)
                    return new ValidationResult(false, "This field is necessary!");
                if (text.Length >= 8)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Minimum 8 characters!");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
