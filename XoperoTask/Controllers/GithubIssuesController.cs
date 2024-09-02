using Microsoft.AspNetCore.Mvc;
using XoperoLib.Models;
using XoperoLib.Requests;
using XoperoLib.Services;

namespace XoperoTask.Controllers
{
    [ApiController]
    [Route("github/issues")]
    public class GithubIssuesController : ControllerBase
    {
        private readonly ILogger<GithubIssuesController> _logger;
        private readonly IGitHubServiceClient _gitHubServiceClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public GithubIssuesController(
            ILogger<GithubIssuesController> logger, 
            IGitHubServiceClient gitHubServiceClient,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _gitHubServiceClient = gitHubServiceClient;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "Get GitHub Issues")]
        public async Task<ActionResult<IEnumerable<GithubIssue>>> Get()
        {
            var response = await _gitHubServiceClient.GetIssues();

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGithubIssues(response));
        }

        [HttpPost(Name = "CreateIssue")]
        public async Task<ActionResult<GithubIssue>> Create([FromBody] CreateGithubIssueRequest request)
        {
            var response = await _gitHubServiceClient.CreateIssue(request);
            
            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGithubIssue(response));
        }

        [HttpPatch(Name = "UpdateIssue")]
        public async Task<ActionResult<GithubIssue>> Update([FromBody] UpdateGithubIssueRequest request)
        {
            var response = await _gitHubServiceClient.UpdateIssue(request);

            if (!response.IsSuccessStatusCode)
                return NotFound();
            return Ok(XoperoLib.Extensions.ResponseExtensions.DeserializeGithubIssue(response));
        }

        [HttpDelete(Name = "DeleteIssue")]
        public async Task<ActionResult> Delete([FromBody] DeleteGithubIssueRequest request)
        {
            var response = await _gitHubServiceClient.DeleteIssue(request);

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                return NotFound();
            else return Ok();
        }
    }
}