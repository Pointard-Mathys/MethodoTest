using Processor.Contracts;
using Processor.Model;

namespace Processor.Services;

public class PasswordValidator : IPasswordValidator
{
    public ValidationResult Validate(string password)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrEmpty(password))
        {
            result.AddError("Le mot de passe ne peut pas être vide");
            return result;
        }
        
        if (password.Length < 8)
            result.AddError("Le mot de passe doit contenir au moins 8 caractères");
            
        if (!password.Any(char.IsUpper))
            result.AddError("Le mot de passe doit contenir au moins une majuscule");
            
        if (!password.Any(char.IsLower))
            result.AddError("Le mot de passe doit contenir au moins une minuscule");
            
        if (!password.Any(char.IsDigit))
            result.AddError("Le mot de passe doit contenir au moins un chiffre");
            
        return result;
    }
}