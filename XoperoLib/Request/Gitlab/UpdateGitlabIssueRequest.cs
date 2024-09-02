namespace XoperoLib.Request.Gitlab
{
    public class UpdateGitlabIssueRequest
    {
        /// <summary>
        /// The global ID or URL-encoded path of the project owned by the authenticated user.
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// The internal ID of a project’s issue.
        /// </summary>
        public int IssueIid { get; set; }
        /// <summary>
        /// The title of an issue.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of an issue. Limited to 1,048,576 characters.
        /// </summary>
        public string Description { get; set; }
    }
}