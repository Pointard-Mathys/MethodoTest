using Processor.Model;

namespace Processor.Contracts;

public interface IPasswordValidator
{
    /// <summary>
    /// Validates the given password according to specified criteria.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>A ValidationResult instance containing validation errors, if any, and the validity status.</returns>
    public ValidationResult Validate(string password);
}