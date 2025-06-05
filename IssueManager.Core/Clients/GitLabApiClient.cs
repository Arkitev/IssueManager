using IssueManager.Core.Interfaces;
using IssueManager.Core.Models;
using IssueManager.Core.Models.Responses;
using IssueManager.Core.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IssueManager.Core.Services
{
    public class GitLabApiClient : IGitLabApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly GitLabOptions _options;

        public GitLabApiClient(HttpClient httpClient, IOptions<GitLabOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<Issue> CreateIssueAsync(string title, string? description)
        {
            var url = $"projects/{_options.ProjectId}/issues";
            var payload = new
            {
                title,
                description
            };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createdIssue = JsonSerializer.Deserialize<GitLabCreateIssueResponse>(jsonResponse)
                ?? throw new InvalidOperationException("Failed to deserialize GitLab issue response.");

            return new Issue
            {
                Id = createdIssue.iid.ToString(),
                Title = createdIssue.title,
                Description = createdIssue.description
            };
        }

        public async Task<bool> UpdateIssueAsync(int id, string? title, string? description)
        {
            var url = $"projects/{_options.ProjectId}/issues/{id}";
            var payload = new
            {
                title,
                description
            };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CloseIssueAsync(int id)
        {
            var url = $"projects/{_options.ProjectId}/issues/{id}";
            var payload = new
            {
                state_event = "close"
            };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}
