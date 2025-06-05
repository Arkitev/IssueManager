namespace IssueManager.Core.Options;

public class GitLabOptions
{
    public string BaseUrl { get; set; } = null!;
    public string ProjectId { get; set; } = null!;
    public string AuthToken { get; set; } = null!;
}
