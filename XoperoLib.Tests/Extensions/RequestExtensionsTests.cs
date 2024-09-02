using XoperoLib.Extensions;

namespace XoperoLib.Tests.Extensions
{
    public class RequestExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateGithubIssueRequestContent_NullArgument_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                RequestExtensions.CreateGithubIssueRequestContent(null));
        }

        [Test]
        public void UpdateGithubIssueRequestContent_NullArgument_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                RequestExtensions.UpdateGithubIssueRequestContent(null));
        }
    }
}
