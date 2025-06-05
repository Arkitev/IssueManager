namespace IssueManager.Core.Models.Responses;

public record GitLabCreateIssueResponse
{
    public int id { get; init; }
    public int iid { get; init; }
    public string title { get; init; } = null!;
    public string? description { get; init; }
}
