namespace trello_services.Models.Request
{
    public class LabelRequestModel
    {
        public string? labelName { get; set; }
        public string? theme { get; set; }
        public Guid boardId { get; set; }
    }
}
