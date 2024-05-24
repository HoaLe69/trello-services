namespace trello_services.Models.Response
{
    public class WorkspaceResponseModel
    {
        public Guid workSpaceId { get; set; }
        public string title { get; set; }
        public string theme { get; set; }
        public string? description { get; set; }
    }
}
