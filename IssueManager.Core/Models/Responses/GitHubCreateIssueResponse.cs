namespace IssueManager.Core.Models.Responses;

public record GitHubCreateIssueResponse
{
    public int number { get; init; }
    public string title { get; init; } = null!;
    public string? body { get; init; }
}
