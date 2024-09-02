using System.Text.Json.Serialization;

namespace XoperoLib.Requests
{
    public class DeleteGithubIssueRequest
    {
        #region Path parameters (required)

        /// <summary>
        /// The account owner of the repository. The name is not case sensitive.
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// The name of the repository without the .git extension. The name is not case sensitive.
        /// </summary>
        public string Repo { get; set; }
        /// <summary>
        /// The number that identifies the issue.
        /// </summary>
        [JsonPropertyName("issue_number")]
        public string IssueNumber { get; set; }

        #endregion
    }
}