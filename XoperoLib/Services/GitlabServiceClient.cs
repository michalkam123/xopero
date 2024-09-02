using XoperoLib.Request.Gitlab;

namespace XoperoLib.Services
{
    public class GitlabServiceClient : IGitlabServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string GetUri = "issues";
        private string CreateUri = "projects/{0}/issues?title={1}&description={2}";
        private string UpdateUri = "projects/{0}/issues/{1}?title={2}&description={3}";
        private string DeleteUri = "projects/{0}/issues/{1}";

        public GitlabServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient GetClient()
        {
            return _httpClientFactory.CreateClient("GitLabServiceClient");
        }

        public async Task<HttpResponseMessage> GetIssues()
        {
            return await GetClient().SendAsync(
                new HttpRequestMessage(HttpMethod.Get, GetUri));
        }

        public async Task<HttpResponseMessage> CreateIssue(CreateGitlabIssueRequest request)
        {
            var uri = string.Format(CreateUri, request.ProjectId, request.Title, request.Description);

            return await GetClient().SendAsync(
                new HttpRequestMessage(HttpMethod.Post, uri));
        }

        public async Task<HttpResponseMessage> DeleteIssue(DeleteGitlabIssueRequest request)
        {
            var uri = string.Format(DeleteUri,
                request.ProjectId, request.IssueIid);

            return await GetClient().SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, uri));
        }

        public async Task<HttpResponseMessage> UpdateIssue(UpdateGitlabIssueRequest request)
        {
            var uri = string.Format(UpdateUri, 
                request.ProjectId, request.IssueIid, request.Title, request.Description);

            return await GetClient().SendAsync(
                new HttpRequestMessage(HttpMethod.Put, uri));
        }
    }
}