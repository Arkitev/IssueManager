using System.ComponentModel.DataAnnotations;

namespace IssueManager.API.Requests;

public record CloseIssueRequest
{
    [Required]
    public int Id { get; init; }
}
