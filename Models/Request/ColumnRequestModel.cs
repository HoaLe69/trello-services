namespace trello_services.Models.Request
{
    public class ListCardRequestModel
    {
        public string? title { get; set; }
        public string? orderCardIds { get; set; }
        public Guid? boardId { get; set; }
    }
}
