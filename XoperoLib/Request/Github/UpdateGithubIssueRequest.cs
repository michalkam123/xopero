using System.Text.Json.Serialization;

namespace XoperoLib.Requests
{
    public class UpdateGithubIssueRequest
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
        /// The open or closed state of the issue.
        /// </summary>
        public string? State { get; set; }
        /// <summary>
        /// The reason for the state change. Ignored unless state is changed.
        /// </summary>
        [JsonPropertyName("state_reason")]
        public string? StateReason { get; set; }
        /// <summary>
        /// The number of the milestone to associate this issue with or use null to remove the current milestone. Only users with push access can set the milestone for issues. Without push access to the repository, milestone changes are silently dropped.
        /// </summary>
        public string? Milestone { get; set; }
        /// <summary>
        /// Labels to associate with this issue. Pass one or more labels to replace the set of labels on this issue. Send an empty array ([]) to clear all labels from the issue. Only users with push access can set labels for issues. Without push access to the repository, label changes are silently dropped.
        /// </summary>
        public IEnumerable<string>? Labels { get; set; }
        /// <summary>
        /// Usernames to assign to this issue. Pass one or more user logins to replace the set of assignees on this issue. Send an empty array ([]) to clear all assignees from the issue. Only users with push access can set assignees for new issues. Without push access to the repository, assignee changes are silently dropped.
        /// </summary>
        public IEnumerable<string>? Assignees { get; set; }

        #endregion
    }
}