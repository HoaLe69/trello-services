namespace trello_services.Entities
{
    public class Comment
    {
        public Int64 commentId { get; set; }
        public string content { get; set; }
        public Guid userId { get; set; }
        public Guid cardId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
        public DateTime? createAt { get; set; }
        public Comment()
        {
            createAt = DateTime.Now;
        }
    }
}
