using System.ComponentModel.DataAnnotations;

namespace Puroguramu.App.Areas.Identity.Pages.Account.Validators;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var file = value as IFormFile;

        if (file == null)
        {
            throw new ArgumentException("Invalid type");
        }

        if (file.Length > _maxFileSize)
        {
            return new ValidationResult($"The file exceeds the {_maxFileSize / 1024} KB limit.");
        }

        return ValidationResult.Success;
    }
}
