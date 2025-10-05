using System.Threading.Tasks;
using Xunit;

namespace ContosoUniversityIntegrationTests;

public class SmokeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public SmokeTests(CustomWebApplicationFactory factory) => _factory = factory;

    [Theory]
    [InlineData("/")]             // Home
    [InlineData("/Students")]     // Students
    [InlineData("/Courses")]      // Courses
    [InlineData("/Enrollments")]  // Enrollments list page
    public async Task Pages_Render_OK(string url)
    {
        var client = _factory.CreateClient();
        var resp = await client.GetAsync(url);
        resp.EnsureSuccessStatusCode(); // 200
        var html = await resp.Content.ReadAsStringAsync();
        Assert.False(string.IsNullOrWhiteSpace(html));
    }
}
