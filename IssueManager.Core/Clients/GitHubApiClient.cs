using IssueManager.Core.Interfaces;
using IssueManager.Core.Models;
using IssueManager.Core.Models.Responses;
using IssueManager.Core.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IssueManager.Core.Services;

public class GitHubApiClient : IGitHubApiClient
{
    private readonly HttpClient _httpClient;
    private readonly GitHubOptions _options;

    public GitHubApiClient(HttpClient httpClient, IOptions<GitHubOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<Issue> CreateIssueAsync(string title, string? description)
    {
        var url = $"/repos/{_options.RepoOwner}/{_options.RepoName}/issues";
        var payload = new
        {
            title,
            body = description
        };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var createdIssue = JsonSerializer.Deserialize<GitHubCreateIssueResponse>(jsonResponse)
            ?? throw new InvalidOperationException("Failed to deserialize GitHub issue response.");

        return new Issue
        {
            Id = createdIssue.number.ToString(),
            Title = createdIssue.title,
            Description = createdIssue.body
        };
    }

    public async Task<bool> UpdateIssueAsync(int id, string? title, string? description)
    {
        var url = $"/repos/{_options.RepoOwner}/{_options.RepoName}/issues/{id}";
        var payload = new
        {
            title,
            body = description
        };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(url, content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CloseIssueAsync(int id)
    {
        var url = $"/repos/{_options.RepoOwner}/{_options.RepoName}/issues/{id}";
        var payload = new
        {
            state = "closed"
        };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(url, content);

        return response.IsSuccessStatusCode;
    }
}
