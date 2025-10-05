using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContosoUniversityIntegrationTests;

public static class TestHelpers
{
    // Returns ("__RequestVerificationToken", "theTokenValue")
    public static async Task<(string tokenName, string tokenValue)> GetAntiforgeryAsync(HttpClient client, string formUrl)
    {
        var get = await client.GetAsync(formUrl);
        get.EnsureSuccessStatusCode();
        var html = await get.Content.ReadAsStringAsync();

        // Simple regex for: <input name="__RequestVerificationToken" value="...">
        var match = Regex.Match(html, "name=\"(__RequestVerificationToken)\".*?value=\"([^\"]+)\"",
                                RegexOptions.Singleline | RegexOptions.IgnoreCase);
        if (!match.Success)
            throw new InvalidOperationException($"Anti-forgery token not found at {formUrl}");

        return (match.Groups[1].Value, match.Groups[2].Value);
    }
}
