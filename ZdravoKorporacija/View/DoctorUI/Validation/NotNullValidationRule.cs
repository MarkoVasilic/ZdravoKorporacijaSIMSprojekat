using System;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.DoctorUI.Validation
{
    public class NotNullValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (String.IsNullOrWhiteSpace(text))
                    return new ValidationResult(false, "This field is necessary!");
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured!");
            }

        }
    }
}
