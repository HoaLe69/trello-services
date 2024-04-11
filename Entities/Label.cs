namespace trello_services.Entities
{
    public class Label
    {
        public Guid labelId {  get; set; }
        public string? labelName { get; set; }  
        public string theme { get; set; }
        public Guid boardId { get; set; }
        public Board Board { get; set; }
        public IList<CardLabel> CardLabels { get; set; }
        public Label()
        {
            CardLabels = new List<CardLabel>();
        }
    }
}
