using ContosoUniversity.Services;
using Xunit;

public class StudentProgressServiceTests
{
    // Arrange Step for service tests
    private readonly StudentProgressService _svc = new();

    // DisplayName Tests
    [Fact]
    public void DisplayName_FormatsProperly()
        => Assert.Equal("Ada Lovelace", _svc.DisplayName("Ada", "Lovelace"));

    // ComputeGpa Tests
    [Fact]
    public void ComputeGpa_CalculatesWeightedAverage()
        => Assert.Equal(3.5, _svc.ComputeGpa((3, 4.0), (3, 3.0)), 3);
}
