using System.ComponentModel.DataAnnotations;

namespace IssueManager.API.Requests;

public record CreateIssueRequest
{
    [Required]
    public string Title { get; init; } = null!;

    public string? Description { get; init; }
}
