namespace trello_services.Entities
{
    public class CardLabel
    {
        public Guid cardId { get; set; }
        public Guid  labelId { get; set; }
        public Label Label { get; set;}
        public Card Card { get; set;}
    }
}
