using IssueManager.Core.Interfaces;
using IssueManager.Core.Models;

namespace IssueManager.Core.Services;

public class GitLabIssueService : IIssueService
{
    private readonly IGitLabApiClient _gitLabApiClient;

    public GitLabIssueService(IGitLabApiClient gitLabApiClient)
    {
        _gitLabApiClient = gitLabApiClient;
    }

    public async Task<Issue> CreateIssueAsync(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        return await _gitLabApiClient.CreateIssueAsync(title, description);
    }

    public async Task<bool> UpdateIssueAsync(int id, string? title, string? description)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be greater than zero.", nameof(id));

        return await _gitLabApiClient.UpdateIssueAsync(id, title, description);
    }

    public async Task<bool> CloseIssueAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id must be greater than zero.", nameof(id));

        return await _gitLabApiClient.CloseIssueAsync(id);
    }
}
