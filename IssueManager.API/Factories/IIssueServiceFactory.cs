using IssueManager.Core.Enums;
using IssueManager.Core.Interfaces;

namespace IssueManager.API.Factories;

public interface IIssueServiceFactory
{
    IIssueService GetService(GitProvider provider);
}
