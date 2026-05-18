using System.ComponentModel.DataAnnotations;

namespace Puroguramu.App.Areas.Identity.Pages.Account.Validators;

public class CustomEmailValidatorAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return false;
        }

        var email = value.ToString();

        return IsValidEmailFormat(email);
    }

    private bool IsValidEmailFormat(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
