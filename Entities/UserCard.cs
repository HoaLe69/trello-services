namespace trello_services.Entities
{
    public class UserCard
    {
        public Int64 userCardId {  get; set; }  
        public Guid userId { get; set; }
        public Int64 cardId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
    }
}
