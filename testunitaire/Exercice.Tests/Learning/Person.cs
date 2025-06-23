namespace Learning;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public string GetFullName() => $"{FirstName} {LastName}";
    public bool IsAdult() => Age >= 18;
    public bool IsValidAge() => Age >= 0 && Age <= 150;
}