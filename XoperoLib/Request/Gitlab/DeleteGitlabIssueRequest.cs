namespace XoperoLib.Request.Gitlab
{
    public  class DeleteGitlabIssueRequest
    {
        /// <summary>
        /// The global ID or URL-encoded path of the project owned by the authenticated user.
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// The internal ID of a project’s issue.
        /// </summary>
        public int IssueIid { get; set; }
    }
}