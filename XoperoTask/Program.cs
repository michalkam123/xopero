using XoperoLib.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var githubToken = builder.Configuration.GetValue(typeof(string), "GithubToken") ?? throw new Exception("Github token is missing");

        var gitlabToken = builder.Configuration.GetValue(typeof(string), "GitlabToken") ?? throw new Exception("Gitlab token is missing");

        var githubApiUrl = builder.Configuration.GetValue(typeof(string), "GithubApiUri") ?? throw new Exception("Github Api Uri is missing");

        var gitlabApiUrl = builder.Configuration.GetValue(typeof(string), "GitlabApiUri") ?? throw new Exception("Gitlab Api Uri is missing");

        builder.Services.AddHttpClient("GitHubServiceClient", client =>
        {
            client.BaseAddress = new Uri(githubApiUrl.ToString());
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Token", githubToken.ToString());
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            client.DefaultRequestHeaders.Add("User-Agent", "request");
        });

        builder.Services.AddHttpClient("GitLabServiceClient", client =>
        {
            client.BaseAddress = new Uri(gitlabApiUrl.ToString());
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("PRIVATE-TOKEN", gitlabToken.ToString());
        });

        builder.Services.AddScoped<IGitHubServiceClient, GitHubServiceClient>();
        builder.Services.AddScoped<IGitlabServiceClient, GitlabServiceClient>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}