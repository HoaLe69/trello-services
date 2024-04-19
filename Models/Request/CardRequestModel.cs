using trello_services.Entities;

namespace trello_services.Models.Request
{
    public class CardRequestModel
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? cover { get; set; }
        public Int64? columnId { get; set; }
    }
}
