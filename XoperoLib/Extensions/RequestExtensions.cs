using System.Text;
using System.Text.Json;
using XoperoLib.Requests;

namespace XoperoLib.Extensions
{
    public static class RequestExtensions
    {
        public static StringContent CreateGithubIssueRequestContent(CreateGithubIssueRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            return new(
                JsonSerializer.Serialize(new
                {
                    title = request.Title,
                    body = request.Body,
                    milestone = request.Milestone,
                    labels = request.Labels,
                    assignees = request.Assignees
                }),
                Encoding.UTF8,
                "application/vnd.github+json");
        }

        public static StringContent UpdateGithubIssueRequestContent(UpdateGithubIssueRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            return new(
                JsonSerializer.Serialize(new
                {
                    title = request.Title,
                    body = request.Body,
                    state = request.State,
                    state_reason = request.StateReason,
                    milestone = request.Milestone,
                    labels = request.Labels,
                    assignees = request.Assignees
                }),
                Encoding.UTF8,
                "application/vnd.github+json");
        }
    }
}