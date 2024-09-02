using XoperoLib.Requests;
using XoperoLib.Extensions;

namespace XoperoLib.Services
{
    public class GitHubServiceClient : IGitHubServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string GetUri = "issues";
        private string CreateUri = "repos/{0}/{1}/issues";
        private string UpdateUri = "repos/{0}/{1}/issues/{2}";
        private string DeleteUri = "repos/{0}/{1}/issues/{2}/lock";

        public GitHubServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient("GitHubServiceClient");
        }

        public async Task<HttpResponseMessage> GetIssues()
        {
            return await GetClient().SendAsync(
                new HttpRequestMessage(HttpMethod.Get, GetUri));
        }

        public async Task<HttpResponseMessage> CreateIssue(CreateGithubIssueRequest request)
        {
            var uri = string.Format(CreateUri, request.Owner, request.Repo);

            var content = RequestExtensions.CreateGithubIssueRequestContent(request);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };

            return await GetClient().SendAsync(requestMessage); ;
        }

        public async Task<HttpResponseMessage> UpdateIssue(UpdateGithubIssueRequest request)
        {
            var uri = string.Format(UpdateUri, request.Owner, request.Repo, request.IssueNumber);

            var requestMessage = new HttpRequestMessage(HttpMethod.Patch, uri)
            {
                Content = RequestExtensions.UpdateGithubIssueRequestContent(request)
            };

            return await GetClient().SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> DeleteIssue(DeleteGithubIssueRequest request)
        {
            var uri = string.Format(DeleteUri, request.Owner, request.Repo, request.IssueNumber);

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            return await GetClient().SendAsync(requestMessage);
        }
    }
}