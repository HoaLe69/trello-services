namespace trello_services.Entities
{
    public class Activity
    {
        public Int64 activityId {  get; set; }
        public string content { get; set; }

        public DateTime? createAt { get; set; }
        public Int64 cardId { get; set; }
        public Guid userId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
        public Activity()
        {
            createAt = DateTime.Now;    
        }
    }
}
