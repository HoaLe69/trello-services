namespace trello_services.Entities
{
    public class UserCard
    {
        public Guid userId { get; set; }
        public Guid cardId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
    }
}
