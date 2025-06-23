using Management.Model;

namespace Management.Service;

public class StudentManager : IStudentManager
{
    private readonly List<Student> _students = new ();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    
    public void AddStudent(Student student)
    {
        throw new NotImplementedException();
    }

    public Student GetStudentById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Student> GetStudentsByAge(int minAge, int maxAge)
    {
        throw new NotImplementedException();
    }

    public List<Student> GetTopStudents(int count)
    {
        throw new NotImplementedException();
    }

    public bool RemoveStudent(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateStudentGrades(int studentId, List<int> newGrades)
    {
        var student = GetStudentById(studentId);
        if (student == null)
            throw new ArgumentException("Student not found");
            
        student.Grades = newGrades ?? [];
    }
}