using FluentAssertions;
using Management.Model;
using Management.Service;

namespace Management.UnitTest;

//Equivalant avec FluentAssertions :
//Assert.Collection	-> .Should().SatisfyRespectively(...)
//Assert.Empty	-> .Should().BeEmpty()
//Assert.NotEmpty	-> .Should().NotBeEmpty()
//Assert.All(collection, action) -> .Should().OnlyContain(item => /* condition ici */) ->

public class StudentManagerTest
{
    private readonly StudentManager _manager = new();

    /// <summary>
    /// Vérifie qu’un étudiant valide est correctement ajouté à la collection.
    /// </summary>
    [Fact]
    public void AddStudent_ValidStudent_AddsToCollection()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", Age = 20 };

        // Act
        _manager.AddStudent(student);

        // Assert
        _manager.Students.Should().ContainSingle().Which.Should().BeEquivalentTo(student);
    }

    /// <summary>
    /// Vérifie que GetStudentsByAge retourne les bons étudiants dans une tranche d’âge donnée.
    /// </summary>
    [Fact]
    public void GetStudentsByAge_ValidRange_ReturnsCorrectStudents()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 1, Age = 18, FirstName = "John", LastName = "Ynov" });
        _manager.AddStudent(new Student { Id = 2, Age = 20, FirstName = "Doe", LastName = "Ynov" });
        _manager.AddStudent(new Student { Id = 3, Age = 25, FirstName = "Paul", LastName = "Ynov" });

        // Act
        var result = _manager.GetStudentsByAge(18, 22);

        // Assert
        result.Should().SatisfyRespectively(
            student =>
            {
                student.Age.Should().Be(18);
                student.FirstName.Should().Be("John");
                student.LastName.Should().Be("Ynov");
            },
            student =>
            {
                student.Age.Should().Be(20);
                student.FirstName.Should().Be("Doe");
                student.LastName.Should().Be("Ynov");
            }
        );
    }

    /// <summary>
    /// Vérifie qu’ajouter un étudiant null déclenche une exception.
    /// </summary>
    [Fact]
    public void AddStudent_NullStudent_ThrowsArgumentNullException()
    {
        // Act
        var act = () => _manager.AddStudent(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Vérifie qu’ajouter un étudiant avec un ID déjà existant lance une exception.
    /// </summary>
    [Fact]
    public void AddStudent_DuplicateId_ThrowsException()
    {
        // Arrange
        var student = new Student { Id = 1, FirstName = "A", LastName = "B" };
        _manager.AddStudent(student);

        // Act
        Action act = () => _manager.AddStudent(new Student { Id = 1, FirstName = "C", LastName = "D" });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("A student with ID 1 already exists.");
    }

    /// <summary>
    /// Vérifie que GetStudentById retourne l'étudiant attendu quand l'ID existe.
    /// </summary>
    [Fact]
    public void GetStudentById_ExistingId_ReturnsCorrectStudent()
    {
        // Arrange
        var student = new Student { Id = 10, FirstName = "Alice", LastName = "Dupont", Age = 22 };
        _manager.AddStudent(student);

        // Act
        var result = _manager.GetStudentById(10);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("Alice");
    }

    /// <summary>
    /// Vérifie que GetStudentById retourne null quand l'ID n’existe pas.
    /// </summary>
    [Fact]
    public void GetStudentById_NonExistingId_ReturnsNull()
    {
        // Act
        var result = _manager.GetStudentById(999);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Vérifie que RemoveStudent supprime correctement un étudiant existant.
    /// </summary>
    [Fact]
    public void RemoveStudent_ExistingId_RemovesStudent()
    {
        // Arrange
        var student = new Student { Id = 5, FirstName = "Tom", LastName = "Smith", Age = 21 };
        _manager.AddStudent(student);

        // Act
        var removed = _manager.RemoveStudent(5);

        // Assert
        removed.Should().BeTrue();
        _manager.Students.Should().BeEmpty();
    }

    /// <summary>
    /// Vérifie que RemoveStudent retourne false si l’étudiant n’existe pas.
    /// </summary>
    [Fact]
    public void RemoveStudent_NonExistingId_ReturnsFalse()
    {
        // Act
        var removed = _manager.RemoveStudent(1234);

        // Assert
        removed.Should().BeFalse();
    }

    /// <summary>
    /// Vérifie que les notes sont bien mises à jour et que la moyenne est correcte.
    /// </summary>
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
        student.Grades.Should().BeEquivalentTo(newGrades);
        student.AverageGrade.Should().BeApproximately(19, 0.01);
    }
    
    /// <summary>
    /// Vérifie que mettre à jour les notes d’un étudiant inexistant lance une exception.
    /// </summary>
    [Fact]
    public void UpdateStudentGrades_UnknownId_ThrowsException()
    {
        // Act
        Action act = () => _manager.UpdateStudentGrades(999, new List<int> { 10, 12 });

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Student not found");
    }

    /// <summary>
    /// Vérifie que passer des notes nulles ne modifie pas les notes de l'étudiant.
    /// </summary>
    [Fact]
    public void UpdateStudentGrades_NullGrades_DoesNothing()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            FirstName = null,
            LastName = null
        };
        _manager.AddStudent(student);

        // Act
        var act = () => _manager.UpdateStudentGrades(1, null!);

        // Assert
        act.Should().NotThrow();
        student.Grades.Should().BeNullOrEmpty(); // ✔️ adapte à la logique actuelle
    }

    /// <summary>
    /// Vérifie que des notes vides sont acceptées et que la moyenne est 0.
    /// </summary>
    [Fact]
    public void UpdateStudentGrades_EmptyGrades_ResultsInZeroAverage()
    {
        // Arrange
        var student = new Student
        {
            Id = 1,
            FirstName = null,
            LastName = null
        };
        _manager.AddStudent(student);

        // Act
        _manager.UpdateStudentGrades(1, new List<int>());

        // Assert
        student.Grades.Should().BeEmpty();
        student.AverageGrade.Should().Be(0);
    }

    /// <summary>
    /// Vérifie que GetTopStudents retourne les étudiants avec la meilleure moyenne en ordre décroissant.
    /// </summary>
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
        top.Should().HaveCount(2);
        top[0].FullName.Should().Be("Alice A");
        top[1].FullName.Should().Be("Bob B");
    }
    
    /// <summary>
    /// Vérifie que demander un top 0 lève une exception.
    /// </summary>
    [Fact]
    public void GetTopStudents_ZeroCount_ThrowsException()
    {
        // Act
        Action act = () => _manager.GetTopStudents(0);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Count must be greater than 0");
    }

    /// <summary>
    /// Vérifie que si count est plus grand que le nombre d’étudiants, tous sont retournés.
    /// </summary>
    [Fact]
    public void GetTopStudents_CountGreaterThanAvailable_ReturnsAll()
    {
        // Arrange
        _manager.AddStudent(new Student
        {
            Id = 1,
            Grades = new List<int>
            {
                12
            },
            FirstName = null,
            LastName = null
        });
        _manager.AddStudent(new Student
        {
            Id = 2,
            Grades = new List<int>
            {
                18
            },
            FirstName = null,
            LastName = null
        });

        // Act
        var result = _manager.GetTopStudents(10);

        // Assert
        result.Should().HaveCount(2);
    }

    /// <summary>
    /// Vérifie que tous les étudiants sont retournés, même sans notes.
    /// </summary>
    [Fact]
    public void GetTopStudents_IncludesStudentsWithoutGrades()
    {
        // Arrange
        _manager.AddStudent(new Student
        {
            Id = 1,
            FirstName = null,
            LastName = null
        }); // pas de notes
        _manager.AddStudent(new Student
        {
            Id = 2,
            Grades = new List<int> { 20 },
            FirstName = null,
            LastName = null
        });

        // Act
        var result = _manager.GetTopStudents(2);

        // Assert
        result.Should().HaveCount(2);
        result.Select(s => s.Id).Should().Contain(new[] { 1, 2 });
    }

    /// <summary>
    /// Vérifie que la moyenne est 0 lorsqu’un étudiant n’a aucune note.
    /// </summary>
    [Fact]
    public void AverageGrade_EmptyGrades_ReturnsZero()
    {
        // Arrange
        var student = new Student
        {
            Id = 99,
            FirstName = "NoGrade",
            LastName = "Student",
            Grades = []
        };

        // Act
        var average = student.AverageGrade;

        // Assert
        average.Should().Be(0);
    }

    /// <summary>
    /// Vérifie que tous les étudiants retournés sont dans la tranche d’âge spécifiée.
    /// </summary>
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
        result.Should().OnlyContain(s => s.Age >= 18 && s.Age <= 22);
    }

    /// <summary>
    /// Vérifie que GetStudentsByAge retourne une liste vide s’il n’y a aucun étudiant correspondant.
    /// </summary>
    [Fact]
    public void GetStudentsByAge_NoMatches_ReturnsEmptyList()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 1, Age = 12, FirstName = "Mini", LastName = "Moi" });

        // Act
        var result = _manager.GetStudentsByAge(20, 25);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Vérifie qu’au moins un étudiant est retourné si un correspond à la tranche d’âge.
    /// </summary>
    [Fact]
    public void GetStudentsByAge_WithMatches_ReturnsNotEmpty()
    {
        // Arrange
        _manager.AddStudent(new Student { Id = 2, Age = 21, FirstName = "Max", LastName = "Power" });

        // Act
        var result = _manager.GetStudentsByAge(20, 25);

        // Assert
        result.Should().NotBeEmpty();
    }
    
    /// <summary>
    /// Vérifie que minAge > maxAge lève une exception.
    /// </summary>
    [Fact]
    public void GetStudentsByAge_MinGreaterThanMax_ThrowsException()
    {
        // Arrange
        _manager.AddStudent(new Student
        {
            Id = 1,
            Age = 20,
            FirstName = null,
            LastName = null
        });

        // Act
        Action act = () => _manager.GetStudentsByAge(30, 10);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("minAge must be less than or equal to maxAge");
    }

    /// <summary>
    /// Vérifie que la méthode gère correctement une liste vide.
    /// </summary>
    [Fact]
    public void GetStudentsByAge_NoStudents_ReturnsEmpty()
    {
        // Act
        var result = _manager.GetStudentsByAge(18, 25);

        // Assert
        result.Should().BeEmpty();
    }
}