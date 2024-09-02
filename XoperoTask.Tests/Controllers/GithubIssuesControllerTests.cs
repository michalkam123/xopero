using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using System.Text.Json;
using XoperoLib.Models;
using XoperoLib.Requests;
using XoperoLib.Services;
using XoperoTask.Controllers;

namespace XoperoTask.Tests.Controllers
{
    public class GithubIssuesControllerTests
    {

        private Mock<ILogger<GithubIssuesController>> _logger;
        private Mock<IGitHubServiceClient> _gitHubServiceClient;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private GithubIssuesController _controller;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<GithubIssuesController>>(MockBehavior.Strict);
            _gitHubServiceClient = new Mock<IGitHubServiceClient>(MockBehavior.Strict);
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _controller = new GithubIssuesController(
                _logger.Object,
                _gitHubServiceClient.Object,
                _httpClientFactory.Object);
        }

        [Test]
        public void CreateIssue_NoSuccessStatusCode_ReturnNotFound()
        {
            _gitHubServiceClient.Setup(c => c.CreateIssue(It.IsAny<CreateGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Create(new CreateGithubIssueRequest() 
                { Owner = "michal", Repo = "testR" }).Result;
            Assert.IsTrue((result.Result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void CreateIssue_SuccessStatusCode_ReturnIssue()
        {
            _gitHubServiceClient.Setup(c => c.CreateIssue(It.IsAny<CreateGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            id = 223,
                            title = "Title 123",
                            body = "Body 123",
                            Description = "Description 123"
                        }),
                        Encoding.UTF8,
                        "application/vnd.github+json"),
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var result = _controller.Create(new CreateGithubIssueRequest()
            { Owner = "michal", Repo = "testR" }).Result;
            var okResult = (result.Result as OkObjectResult);
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.StatusCode == 200);
            Assert.IsNotNull(okResult.Value);
            var issue = okResult.Value as GithubIssue;
            Assert.IsNotNull(issue);

            Assert.AreEqual(issue.Id, 223);
            Assert.AreEqual(issue.Title, "Title 123");
            Assert.AreEqual(issue.Body, "Body 123");
            Assert.AreEqual(issue.Description, "Description 123");
        }

        [Test]
        public void UpdateIssue_NoSuccessStatusCode_ReturnNotFound()
        {
            _gitHubServiceClient.Setup(c => c.UpdateIssue(It.IsAny<UpdateGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Update(new UpdateGithubIssueRequest()
            { Owner = "michal", Repo = "testR" }).Result;
            Assert.IsTrue((result.Result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void UpdateIssue_SuccessStatusCode_ReturnIssue()
        {
            _gitHubServiceClient.Setup(c => c.UpdateIssue(It.IsAny<UpdateGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            id = 223,
                            title = "Title 123",
                            body = "Body 123",
                            Description = "Description 123"
                        }),
                        Encoding.UTF8,
                        "application/vnd.github+json"),
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var result = _controller.Update(new UpdateGithubIssueRequest()
                { Owner = "michal", Repo = "testR" }).Result;
            var okResult = (result.Result as OkObjectResult);
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.StatusCode == 200);
            Assert.IsNotNull(okResult.Value);
            var issue = okResult.Value as GithubIssue;
            Assert.IsNotNull(issue);

            Assert.AreEqual(issue.Id, 223);
            Assert.AreEqual(issue.Title, "Title 123");
            Assert.AreEqual(issue.Body, "Body 123");
            Assert.AreEqual(issue.Description, "Description 123");
        }

        [Test]
        public void DeleteIssue_NoNoContentStatusCode_ReturnNotFound()
        {
            _gitHubServiceClient.Setup(c => c.DeleteIssue(It.IsAny<DeleteGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Delete(new DeleteGithubIssueRequest()
            { Owner = "michal", Repo = "testR" }).Result;
            Assert.IsTrue((result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void DeleteIssue_NoContentStatusCode_ReturnOk()
        {
            _gitHubServiceClient.Setup(c => c.DeleteIssue(It.IsAny<DeleteGithubIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent
                });

            var result = _controller.Delete(new DeleteGithubIssueRequest()
            { Owner = "michal", Repo = "testR" }).Result;
            Assert.IsTrue((result as OkResult).StatusCode == 200);
        }
    }
}
