namespace trello_services.Models.Request
{
    public class BoardRequestModel
    {
        public string title { get; set; }
        public Guid? workSpaceId { get; set; }
        public string? orderColumnIds { get; set; }
    }
}
