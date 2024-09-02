namespace XoperoLib.Models
{
    public class GitlabIssue
    {
        public Int64 Id { get; set; }
        public Int64 Iid { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
