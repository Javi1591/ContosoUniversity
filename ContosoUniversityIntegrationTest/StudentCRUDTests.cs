using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static ContosoUniversityIntegrationTests.TestHelpers;

namespace ContosoUniversityIntegrationTests;

public class StudentsCrudTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public StudentsCrudTests(CustomWebApplicationFactory factory) => _factory = factory;

    [Fact]
    public async Task Students_EndToEnd_CRUD_Works()
    {
        var client = _factory.CreateClient();

        // Create Validation
        var (tokenName, tokenValue) = await GetAntiforgeryAsync(client, "/Students/Create");
        var createForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>(tokenName, tokenValue),
            new("Student.LastName",      "Lovelace"),
            new("Student.FirstMidName",  "Ada"),
            new("Student.EnrollmentDate","2019-09-01")
        });
        var createResp = await client.PostAsync("/Students/Create", createForm);
        Assert.Equal(HttpStatusCode.Redirect, createResp.StatusCode); // PRG on success

        // Grab ID from DB
        int studentId;
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            var ada = await db.Students.FirstOrDefaultAsync(s => s.LastName == "Lovelace");
            Assert.NotNull(ada);
            studentId = ada!.ID;
        }

        // Edit Verification
        (tokenName, tokenValue) = await GetAntiforgeryAsync(client, $"/Students/Edit?id={studentId}");
        var editForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>(tokenName, tokenValue),
            new("Student.ID",            studentId.ToString()), // Grabbed ID from DB
            new("Student.LastName",      "Byron"),              // Change last name test
            new("Student.FirstMidName",  "Ada"),
            new("Student.EnrollmentDate","2019-09-01")
        });
        var editResp = await client.PostAsync("/Students/Edit", editForm);
        Assert.Equal(HttpStatusCode.Redirect, editResp.StatusCode);

        // Verify edit successful
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            var updated = await db.Students.FirstOrDefaultAsync(s => s.ID == studentId);
            Assert.NotNull(updated);
            Assert.Equal("Byron", updated!.LastName);
        }

        // Delete Verification
        (tokenName, tokenValue) = await GetAntiforgeryAsync(client, $"/Students/Delete?id={studentId}");
        var deleteForm = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>(tokenName, tokenValue),
            new("Student.ID", studentId.ToString())
        });
        var deleteResp = await client.PostAsync("/Students/Delete", deleteForm);
        Assert.Equal(HttpStatusCode.Redirect, deleteResp.StatusCode);

        // Verify Delete successful
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            var exists = await db.Students.AnyAsync(s => s.ID == studentId);
            Assert.False(exists);
        }
    }
}
