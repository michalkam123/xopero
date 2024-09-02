namespace XoperoLib.Requests
{
    public class CreateGithubIssueRequest
    {
        #region Path parameters (required)

        /// <summary>
        /// The account owner of the repository. The name is not case sensitive.
        /// </summary>
        public required string Owner { get; set; }
        /// <summary>
        /// The name of the repository without the .git extension. The name is not case sensitive.
        /// </summary>
        public required string Repo { get; set; }

        #endregion

        #region Body parameters (optional)

        /// <summary>
        /// The title of the issue.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The contents of the issue.
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// The number of the milestone to associate this issue with. NOTE: Only users with push access can set the milestone for new issues. The milestone is silently dropped otherwise.
        /// </summary>
        public string? Milestone { get; set; }
        /// <summary>
        /// Labels to associate with this issue. NOTE: Only users with push access can set labels for new issues. Labels are silently dropped otherwise.
        /// </summary>
        public IEnumerable<string>? Labels { get; set; }
        /// <summary>
        /// Logins for Users to assign to this issue. NOTE: Only users with push access can set assignees for new issues. Assignees are silently dropped otherwise.
        /// </summary>
        public IEnumerable<string>? Assignees { get; set; }

        #endregion
    }
}
