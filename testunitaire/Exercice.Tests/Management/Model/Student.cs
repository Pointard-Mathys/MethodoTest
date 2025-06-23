namespace Management.Model;

public class Student
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
    public List<int> Grades { get; set; } = new ();
    
    public double AverageGrade => Grades.Any() ? Grades.Average() : 0;
    public string FullName => $"{FirstName} {LastName}";

}