using System.ComponentModel.DataAnnotations;

namespace IssueManager.API.Requests;

public record UpdateIssueRequest
{
    [Required]
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }
}
