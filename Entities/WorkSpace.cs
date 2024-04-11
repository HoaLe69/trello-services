namespace trello_services.Entities
{
    public class WorkSpace
    {
        public Guid workSpaceId { get; set; }
        public string title { get; set; }
        public string theme { get; set; }
        public string? description { get; set; }
        public UserWorkspace UserWorkspace { get; set; }
        public ICollection<Board> Boards { get; set; }
        public WorkSpace() { 
            Boards = new List<Board>();
        }
    }
}
