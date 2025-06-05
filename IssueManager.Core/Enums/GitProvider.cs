using System.Text.Json.Serialization;

namespace IssueManager.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GitProvider
{
    GitHub,
    GitLab
}
