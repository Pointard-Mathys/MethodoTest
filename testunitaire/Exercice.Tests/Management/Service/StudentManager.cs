using Management.Contract;
using Management.Model;

namespace Management.Service;

//Logique métier de la gestion des étudiants
public class StudentManager : IStudentManager
{
    private readonly List<Student> _students = new ();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));

        if (_students.Any(s => s.Id == student.Id))
            throw new ArgumentException($"A student with ID {student.Id} already exists.");

        _students.Add(student);
    }

    public Student GetStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public List<Student> GetStudentsByAge(int minAge, int maxAge)
    {
        if (minAge > maxAge)
            throw new ArgumentException("minAge must be less than or equal to maxAge");

        return _students.Where(s => s.Age >= minAge && s.Age <= maxAge).ToList();
    }
    //Ajouter une condition pour éviter d'ajouter des notes négatifs
    public List<Student> GetTopStudents(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Count must be greater than 0");

        return _students
            .OrderByDescending(s => s.AverageGrade)
            .ThenBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .Take(count)
            .ToList();
    }

    public bool RemoveStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null)
            return false;

        _students.Remove(student);
        return true;
    }

    public void UpdateStudentGrades(int studentId, List<int> newGrades)
    {
        var student = GetStudentById(studentId);
        if (student == null)
            throw new ArgumentException("Student not found");

        student.Grades = newGrades ?? [];
    }
}