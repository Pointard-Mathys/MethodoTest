using Management.Model;
using Management.Service;

namespace Management.UnitTest;

public class StudentManagerTest
{
    private readonly StudentManager _manager = new ();
    
    [Fact]
    public void AddStudent_ValidStudent_AddsToCollection()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };
        
        // Act
        _manager.AddStudent(student);
        
        // Assert
        Assert.Single(_manager.Students);
        Assert.Contains(student, _manager.Students);
    }
    
    [Fact]
    public void GetStudentsByAge_ValidRange_ReturnsCorrectStudents()
    {
        // Arrange
        _manager.AddStudent(new Student
        {
            Id = 1,
            Age = 18,
            FirstName = "John",
            LastName = "Ynov"
        });
        _manager.AddStudent(new Student
        {
            Id = 2,
            Age = 20,
            FirstName = "Doe",
            LastName = "Ynov"
        });
        _manager.AddStudent(new Student
        {
            Id = 3,
            Age = 25,
            FirstName = "Paul",
            LastName = "Ynov"
        });
        
        // Act
        var result = _manager.GetStudentsByAge(18, 22);
        
        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, s => Assert.InRange(s.Age, 18, 22));
    }

}