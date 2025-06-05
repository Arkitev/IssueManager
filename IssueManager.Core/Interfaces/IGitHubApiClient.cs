using IssueManager.Core.Models;

namespace IssueManager.Core.Interfaces;

public interface IGitHubApiClient
{
    Task<Issue> CreateIssueAsync(string title, string? description);
    Task<bool> UpdateIssueAsync(int id, string? title, string? description);
    Task<bool> CloseIssueAsync(int id);
}
