using XoperoLib.Request.Gitlab;

namespace XoperoLib.Services
{
    public interface IGitlabServiceClient
    {
        public Task<HttpResponseMessage> GetIssues();
        public Task<HttpResponseMessage> CreateIssue(CreateGitlabIssueRequest request);
        public Task<HttpResponseMessage> UpdateIssue(UpdateGitlabIssueRequest request);
        public Task<HttpResponseMessage> DeleteIssue(DeleteGitlabIssueRequest request);
    }
}