using System.Text.RegularExpressions;
using FluentValidation;

public class ImageValidator : AbstractValidator<string>
{
    public ImageValidator()
    {
        RuleFor(imageBase64 => imageBase64)
            .NotEmpty().WithMessage("Image cannot be empty.")
            .Must(BeValidBase64).WithMessage("Invalid base64 string for an image.");
    }

    private bool BeValidBase64(string base64String)
    {
        // The regular expression to check if the string is in data URI format and is base64-encoded
        var regex = new Regex(@"^data:(image\/[a-zA-Z+]+);base64,([^\s]+)$");

        // Check if the string matches the regular expression
        return regex.IsMatch(base64String);
    }
}

class Program
{
    static void Main()
    {
        var imageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA..."; // Replace with your base64 string

        var validator = new ImageValidator();
        var result = validator.Validate(imageBase64);

        if (result.IsValid)
        {
            Console.WriteLine("Validation passed.");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
    }
}