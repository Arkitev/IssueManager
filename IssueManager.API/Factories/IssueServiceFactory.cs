using IssueManager.Core.Enums;
using IssueManager.Core.Interfaces;
using IssueManager.Core.Services;

namespace IssueManager.API.Factories;

public class IssueServiceFactory : IIssueServiceFactory
{
    private readonly GitHubIssueService _githubService;
    private readonly GitLabIssueService _gitlabService;

    public IssueServiceFactory(GitHubIssueService githubService, GitLabIssueService gitlabService)
    {
        _githubService = githubService;
        _gitlabService = gitlabService;
    }

    public IIssueService GetService(GitProvider provider) =>
        provider switch
        {
            GitProvider.GitHub => _githubService,
            GitProvider.GitLab => _gitlabService,
            _ => throw new InvalidOperationException($"Unsupported Git provider: {provider}")
        };
}
