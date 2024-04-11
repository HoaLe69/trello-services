namespace trello_services.Entities
{
    public class Board
    {
        public Guid boardId { get; set; }
        public string title { get; set; }
        public Guid workSpaceId { get; set; }
        public string? orderColumnIds { get; set; }
        public bool star {  get; set; }
        public IList<Column> Columns { get; set; }
        public IList<Label> Labels { get; set; }
        public WorkSpace WorkSpace { get; set; }
        public IList<UserBoard> UserBoards { get; set; }
        public Board()
        {
            Columns = new List<Column>();
            Labels = new List<Label>();
            UserBoards  = new List<UserBoard>();
            star = false;
        }
    }
}
