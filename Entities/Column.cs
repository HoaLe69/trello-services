namespace trello_services.Entities
{
    public class ListCard
    {
        public Guid columnId { get; set; }
        public string title { get; set; }
        public string? orderCardIds { get; set; }
        public Guid boardId { get; set; }
        public Board Board { get; set; }
        public IList<Card> Cards { get; set; }
        public ListCard()
        {
            Cards = new List<Card>();
        }
    }
}
