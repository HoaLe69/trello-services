namespace trello_services.Models.Response
{
    public class CardResponseVM
    {
        public Guid cardId { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string? cover { get; set; }
        public bool? isDueDayComplete { get; set; }
        public Int64 columnId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
