using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using System.Text.Json;
using XoperoLib.Models;
using XoperoLib.Request.Gitlab;
using XoperoLib.Services;
using XoperoTask.Controllers;

namespace XoperoTask.Tests.Controllers
{
    public class GitlabIssuesControllerTests
    {
        private Mock<ILogger<GithubIssuesController>> _logger;
        private Mock<IGitlabServiceClient> _gitLabServiceClient;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private GitlabIssuesController _controller;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<GithubIssuesController>>(MockBehavior.Strict);
            _gitLabServiceClient = new Mock<IGitlabServiceClient>(MockBehavior.Strict);
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _controller = new GitlabIssuesController(
                _logger.Object,
                _gitLabServiceClient.Object,
                _httpClientFactory.Object);
        }

        [Test]
        public void CreateIssue_NoSuccessStatusCode_ReturnNotFound()
        {
            _gitLabServiceClient.Setup(c => c.CreateIssue(It.IsAny<CreateGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Create(new CreateGitlabIssueRequest()
            { 
                ProjectId = "2341", Title = "Test title 2341", Description = "Test Description2341" 
            }).Result;
            Assert.IsTrue((result.Result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void CreateIssue_SuccessStatusCode_ReturnIssue()
        {
            _gitLabServiceClient.Setup(c => c.CreateIssue(It.IsAny<CreateGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            id = 223,
                            title = "Title 123",
                            iid = 456,
                            Description = "Description 123"
                        })),
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var result = _controller.Create(new CreateGitlabIssueRequest()
            {
                ProjectId = "2341",
                Title = "Test title 2341",
                Description = "Test Description2341"
            }).Result;
            var okResult = (result.Result as OkObjectResult);
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.StatusCode == 200);
            Assert.IsNotNull(okResult.Value);
            var issue = okResult.Value as GitlabIssue;
            Assert.IsNotNull(issue);

            Assert.AreEqual(issue.Id, 223);
            Assert.AreEqual(issue.Title, "Title 123");
            Assert.AreEqual(issue.Iid, 456);
            Assert.AreEqual(issue.Description, "Description 123");
        }

        [Test]
        public void UpdateIssue_NoSuccessStatusCode_ReturnNotFound()
        {
            _gitLabServiceClient.Setup(c => c.UpdateIssue(It.IsAny<UpdateGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Update(new UpdateGitlabIssueRequest()
            {
                ProjectId = "2341",
                Title = "Test title 2341",
                Description = "Test Description2341"
            }).Result;
            Assert.IsTrue((result.Result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void UpdateIssue_SuccessStatusCode_ReturnIssue()
        {
            _gitLabServiceClient.Setup(c => c.UpdateIssue(It.IsAny<UpdateGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new
                        {
                            id = 223,
                            title = "Title 123",
                            iid = 456,
                            Description = "Description 123"
                        }),
                        Encoding.UTF8,
                        "application/vnd.github+json"),
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var result = _controller.Update(new UpdateGitlabIssueRequest()
            {
                ProjectId = "2341",
                Title = "Test title 2341",
                Description = "Test Description2341"
            }).Result;
            var okResult = (result.Result as OkObjectResult);
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.StatusCode == 200);
            Assert.IsNotNull(okResult.Value);
            var issue = okResult.Value as GitlabIssue;
            Assert.IsNotNull(issue);

            Assert.AreEqual(issue.Id, 223);
            Assert.AreEqual(issue.Title, "Title 123");
            Assert.AreEqual(issue.Iid, 456);
            Assert.AreEqual(issue.Description, "Description 123");
        }

        [Test]
        public void DeleteIssue_NoNoContentStatusCode_ReturnNotFound()
        {
            _gitLabServiceClient.Setup(c => c.DeleteIssue(It.IsAny<DeleteGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });

            var result = _controller.Delete(new DeleteGitlabIssueRequest()
            { ProjectId = "2341", IssueIid = 4 }).Result;
            Assert.IsTrue((result as NotFoundResult).StatusCode == 404);
        }

        [Test]
        public void DeleteIssue_NoContentStatusCode_ReturnOk()
        {
            _gitLabServiceClient.Setup(c => c.DeleteIssue(It.IsAny<DeleteGitlabIssueRequest>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent
                });

            var result = _controller.Delete(new DeleteGitlabIssueRequest()
            { ProjectId = "2341", IssueIid = 4 }).Result;
            Assert.IsTrue((result as OkResult).StatusCode == 200);
        }
    }
}
