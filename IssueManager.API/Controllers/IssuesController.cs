using IssueManager.API.Factories;
using IssueManager.API.Requests;
using IssueManager.Core.Enums;
using IssueManager.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssueServiceFactory _issueServiceFactory;

    public IssuesController(IIssueServiceFactory issueServiceFactory)
    {
        _issueServiceFactory = issueServiceFactory;
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(Issue), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateIssue([FromQuery] GitProvider provider, [FromBody] CreateIssueRequest request)
    {
        var issueService = _issueServiceFactory.GetService(provider);
        var issue = await issueService.CreateIssueAsync(request.Title, request.Description);

        return Created($"/api/issues/{issue.Id}", issue);
    }

    [HttpPatch("Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateIssue([FromQuery] GitProvider provider, [FromBody] UpdateIssueRequest request)
    {
        var issueService = _issueServiceFactory.GetService(provider);
        var success = await issueService.UpdateIssueAsync(request.Id, request.Title, request.Description);

        if (!success)
            return NotFound($"Issue with ID {request.Id} not found or update failed.");

        return NoContent();
    }

    [HttpPatch("Close")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseIssue([FromQuery] GitProvider provider, [FromBody] CloseIssueRequest request)
    {
        var issueService = _issueServiceFactory.GetService(provider);
        var success = await issueService.CloseIssueAsync(request.Id);

        if (!success)
            return NotFound($"Issue with ID {request.Id} not found or already closed.");

        return NoContent();
    }
}
