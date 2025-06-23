using Management.Model;

namespace Management.Service;

public interface IStudentManager
{
    /// <summary>
    /// Adds a new student to the system.
    /// </summary>
    /// <param name="student">The student object containing the details of the student to be added.</param>
    public void AddStudent(Student student);

    /// <summary>
    /// Retrieves a student by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to retrieve.</param>
    /// <returns>The student object associated with the specified identifier.</returns>
    public Student GetStudentById(int id);

    /// <summary>
    /// Retrieves a list of students whose ages fall within the specified range.
    /// </summary>
    /// <param name="minAge">The minimum age of the students to retrieve.</param>
    /// <param name="maxAge">The maximum age of the students to retrieve.</param>
    /// <returns>A list of students matching the specified age range.</returns>
    public List<Student> GetStudentsByAge(int minAge, int maxAge);

    /// <summary>
    /// Retrieves a list of top-performing students based on their average grades.
    /// </summary>
    /// <param name="count">The number of top students to retrieve.</param>
    /// <returns>A list of the top-performing students.</returns>
    public List<Student> GetTopStudents(int count);

    /// <summary>
    /// Removes a student from the system by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to remove.</param>
    /// <returns>True if the student was successfully removed; otherwise, false.</returns>
    public bool RemoveStudent(int id);

    /// <summary>
    /// Updates the grades of a specific student.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student whose grades are to be updated.</param>
    /// <param name="newGrades">A list of new grades to assign to the student.</param>
    public void UpdateStudentGrades(int studentId, List<int> newGrades);
}