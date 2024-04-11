namespace trello_services.Entities
{
    public class CheckListDetail
    {
        public Int64 clDetailId { get; set; }
        public string content { get; set; }
        public bool status { get; set; }
        public Guid checkListId { get; set; }
        public CheckList CheckList { get; set; }
        public CheckListDetail() { 
            status = false;
        }
    }
}
