namespace trello_services.Entities
{
    public class CheckList
    {
        public Guid checkListId { get; set; }
        public string title { get; set; }
        public Int64 cardId { get; set; }
        public Card card { get; set; }
        public IList<CheckListDetail> CheckListDetails { get; set; }
        public CheckList()
        {
            CheckListDetails = new List<CheckListDetail>();
        }
    }
}
