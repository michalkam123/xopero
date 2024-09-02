using Microsoft.AspNetCore.Mvc;
using XoperoLib.Models;
using XoperoLib.Request.Gitlab;
using XoperoLib.Services;

namespace XoperoTask.Controllers
{
    [ApiController]
    [Route("gitlab/issues")]
    public class GitlabIssuesController : ControllerBase
    {
        private readonly ILogger<GithubIssuesController> _logger;
        private readonly IGitlabServiceClient _gitlabServiceClient;

        private readonly IHttpClientFactory _httpClientFactory;

        public GitlabIssuesController(
            ILogger<GithubIssuesController> logger,
            IGitlabServiceClient gitlabServiceClient,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _gitlabServiceClient = gitlabServiceClient;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "GetGitlabIssues")]
        public async Task<ActionResult<IEnumerable<GitlabIssue>>> Get()
        {
            var response = await _gitlabServiceClient.GetIssues();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGitlabIssues(response));
        }

        [HttpPost(Name = "CreateGitlabIssue")]
        public async Task<ActionResult<GitlabIssue>> Create([FromBody] CreateGitlabIssueRequest request)
        {
            var response = await _gitlabServiceClient.CreateIssue(request);

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGitlabIssue(response));
        }

        [HttpPut(Name = "UpdateGitlabIssue")]
        public async Task<ActionResult<GitlabIssue>> Update([FromBody] UpdateGitlabIssueRequest request)
        {
            var response = await _gitlabServiceClient.UpdateIssue(request);

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGitlabIssue(response));
        }

        [HttpDelete(Name = "DeleteGitlabIssue")]
        public async Task<ActionResult> Delete([FromBody] DeleteGitlabIssueRequest request)
        {
            var response = await _gitlabServiceClient.DeleteIssue(request);

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok();
        }
    }
}
