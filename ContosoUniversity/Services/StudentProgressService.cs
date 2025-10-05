namespace ContosoUniversity.Services;

// Interface for student progress services
public interface IStudentProgressService
{
    // Calculates GPA
    double ComputeGpa(params (int Credits, double GradePoints)[] courses);

    // Formats display name as string
    string DisplayName(string first, string last);
}

// Implements student progress services interface
public class StudentProgressService : IStudentProgressService
{
    // Getting GPA through credits and grade points
    public double ComputeGpa(params (int Credits, double GradePoints)[] courses)
        => courses.Length == 0 ? 0 :
           courses.Sum(c => c.Credits * c.GradePoints) /
           Math.Max(1, courses.Sum(c => c.Credits));

    // Gets and Displays student name
    public string DisplayName(string first, string last)
        => string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(last)
           ? "Unknown"
           : $"{first?.Trim()} {last?.Trim()}".Trim();
}
