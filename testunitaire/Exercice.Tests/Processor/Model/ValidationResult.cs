namespace Processor.Model;

public class ValidationResult
{
    public List<string> Errors { get; } = new ();
    public bool IsValid => !Errors.Any();
    
    public void AddError(string error)
    {
        Errors.Add(error);
    }

}