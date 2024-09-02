using XoperoLib.Requests;

namespace XoperoLib.Services
{
    public interface IGitHubServiceClient
    {
        public Task<HttpResponseMessage> GetIssues();
        public Task<HttpResponseMessage> CreateIssue(CreateGithubIssueRequest request);
        public Task<HttpResponseMessage> UpdateIssue(UpdateGithubIssueRequest request);
        public Task<HttpResponseMessage> DeleteIssue(DeleteGithubIssueRequest request);
    }
}