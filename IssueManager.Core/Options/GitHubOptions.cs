namespace IssueManager.Core.Options;

public class GitHubOptions
{
    public string BaseUrl { get; set; } = null!;
    public string RepoOwner { get; set; } = null!;
    public string RepoName { get; set; } = null!;
    public string AuthToken { get; set; } = null!;
}
