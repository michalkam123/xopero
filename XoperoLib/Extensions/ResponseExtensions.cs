using System.Text.Json;
using XoperoLib.Models;

namespace XoperoLib.Extensions
{
    public static class ResponseExtensions
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static GithubIssue DeserializeGithubIssue(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<GithubIssue>(
                response.Content.ReadAsStream(), jsonSerializerOptions);
        }

        public static IEnumerable<GithubIssue> DeserializeGithubIssues(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<IEnumerable<GithubIssue>>(
                response.Content.ReadAsStream(), jsonSerializerOptions);
        }

        public static GitlabIssue DeserializeGitlabIssue(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<GitlabIssue>(
                response.Content.ReadAsStream(), jsonSerializerOptions);
        }

        public static IEnumerable<GitlabIssue> DeserializeGitlabIssues(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<IEnumerable<GitlabIssue>>(
                response.Content.ReadAsStream(), jsonSerializerOptions);
        }
    }
}