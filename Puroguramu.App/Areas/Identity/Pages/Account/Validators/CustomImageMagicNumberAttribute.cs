using System.ComponentModel.DataAnnotations;

namespace Puroguramu.App.Areas.Identity.Pages.Account.Validators;

public class ImageMagicNumberAttribute : ValidationAttribute
{
    private readonly byte[] _validMagicNumber;

    public ImageMagicNumberAttribute(byte[] validMagicNumber)
    {
        _validMagicNumber = validMagicNumber;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Copiez les premiers bytes de l'image téléchargée dans un MemoryStream
                (value as IFormFile).CopyTo(memoryStream);

                // Récupérez les premiers bytes de l'image
                var uploadedFileBytes = memoryStream.ToArray();

                // Vérifiez si les premiers bytes de l'image correspondent au numéro magique attendu
                if (!IsMagicNumberValid(uploadedFileBytes))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }

        return ValidationResult.Success;
    }

    private bool IsMagicNumberValid(byte[] uploadedFileBytes)
    {
        // Comparez les premiers bytes de l'image avec le numéro magique valide
        return _validMagicNumber.SequenceEqual(uploadedFileBytes.Take(_validMagicNumber.Length));
    }
}

