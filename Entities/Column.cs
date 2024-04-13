using System.ComponentModel.DataAnnotations;

namespace trello_services.Entities
{
    public class Column
    {
        public Int64 columnId { get; set; }
        public string title { get; set; }
        public string? orderCardIds { get; set; }
        public Guid boardId { get; set; }
        public Board Board { get; set; }
        public IList<Card> Cards { get; set; }
        public Column()
        {
            Cards = new List<Card>();
        }
    }
}
