// See https://aka.ms/new-console-template for more information

using Learning;

var validator = new EmailValidator();
bool isValid = validator.IsValidEmailWithPattern("exemple@domaine.com");

Console.WriteLine(isValid);