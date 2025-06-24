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


    //La fonction GetStudentByID retourne bien l'etudiant
    [Fact]
    public void GetStudentById_ExistingId_ReturnsCorrectStudent()
    {
        // Arrange
        var student = new Student { Id = 10, FirstName = "Alice", LastName = "Dupont", Age = 22 };
        _manager.AddStudent(student);

        // Act
        var result = _manager.GetStudentById(10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Alice", result.FirstName);
    }


    //Si l'ID n'existe pas ca retourne null
    [Fact]
    public void GetStudentById_NonExistingId_ReturnsNull()
    {
        // Act
        var result = _manager.GetStudentById(999);

        // Assert
        Assert.Null(result);
    }


    //Supprime l'etudiant existant
    [Fact]
    public void RemoveStudent_ExistingId_RemovesStudent()
    {
        // Arrange
        var student = new Student { Id = 5, FirstName = "Tom", LastName = "Smith", Age = 21 };
        _manager.AddStudent(student);

        // Act
        var removed = _manager.RemoveStudent(5);

        // Assert
        Assert.True(removed);
        Assert.Empty(_manager.Students);
    }


    //Si on supprime un etudiant inexistant erreur
    [Fact]
    public void RemoveStudent_NonExistingId_ReturnsFalse()
    {
        // Act
        var removed = _manager.RemoveStudent(1234);

        // Assert
        Assert.False(removed);
    }


    //On verifie si UpdateStudentGrade met bien a jour
    [Fact]
    public void UpdateStudentGrades_ValidStudent_UpdatesGrades()
    {
        // Arrange
        var student = new Student { Id = 2, FirstName = "Eva", LastName = "Laroche", Age = 20 };
        _manager.AddStudent(student);
        var newGrades = new List<int> { 18, 19, 20 };

        // Act
        _manager.UpdateStudentGrades(2, newGrades);

        // Assert
        Assert.Equal(newGrades, student.Grades);
        Assert.Equal(19, student.AverageGrade);
    }


    //On regarde si le GetTopStudent renvoie dans le bon ordre
    [Fact]
    public void GetTopStudents_ReturnsHighestAverageInOrder()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 1, FirstName = "Zoe", LastName = "Z", Grades = new() { 12, 14 } });
        _manager.AddStudent(new Student { Id = 2, FirstName = "Alice", LastName = "A", Grades = new() { 19, 20 } });
        _manager.AddStudent(new Student { Id = 3, FirstName = "Bob", LastName = "B", Grades = new() { 15, 16 } });

        // Act
        var top = _manager.GetTopStudents(2);

        // Assert
        Assert.Collection(top,
            s => Assert.Equal("Alice A", s.FullName),
            s => Assert.Equal("Bob B", s.FullName)
        );
    }


    // On verifie que tous les etudiants retournes par GetStudentsByAge ont un age compris entre 18 et 22 inclus.
    [Fact]
    public void GetStudentsByAge_AllResultsWithinRange()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 1, Age = 17, FirstName = "Toto", LastName = "T" });
        _manager.AddStudent(new Student { Id = 2, Age = 18, FirstName = "Tata", LastName = "T" });
        _manager.AddStudent(new Student { Id = 3, Age = 22, FirstName = "Titi", LastName = "T" });

        // Act
        var result = _manager.GetStudentsByAge(18, 22);

        // Assert
        Assert.All(result, s => Assert.InRange(s.Age, 18, 22));
    }


    // On teste que si aucun etudiant ne correspond à la tranche d’age demandee, la liste retournee est vide.
    [Fact]
    public void GetStudentsByAge_NoMatches_ReturnsEmptyList()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 1, Age = 12, FirstName = "Mini", LastName = "Moi" });

        // Act
        var result = _manager.GetStudentsByAge(20, 25);

        // Assert
        Assert.Empty(result);
    }


    // On verifie qu’au moins un etudiant est bien retourne quand il correspond à la tranche d’age demandee.
    [Fact]
    public void GetStudentsByAge_WithMatches_ReturnsNotEmpty()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 2, Age = 21, FirstName = "Max", LastName = "Power" });

        // Act
        var result = _manager.GetStudentsByAge(20, 25);

        // Assert
        Assert.NotEmpty(result);
    }

}