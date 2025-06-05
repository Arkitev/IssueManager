using IssueManager.API.Factories;
using IssueManager.API.Middlewares;
using IssueManager.Core.Interfaces;
using IssueManager.Core.Options;
using IssueManager.Core.Services;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection("GitHub"));
builder.Services.AddHttpClient<IGitHubApiClient, GitHubApiClient>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<GitHubOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
    client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AuthToken);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("GitIssueManager");
});

builder.Services.Configure<GitLabOptions>(builder.Configuration.GetSection("GitLab"));
builder.Services.AddHttpClient<IGitLabApiClient, GitLabApiClient>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<GitLabOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AuthToken);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("GitIssueManager");
});

builder.Services.AddScoped<IIssueServiceFactory, IssueServiceFactory>();
builder.Services.AddScoped<GitHubIssueService>();
builder.Services.AddScoped<GitLabIssueService>();

var app = builder.Build();
app.UseExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
