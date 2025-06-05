﻿namespace IssueManager.Core.Models;

public class Issue
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
