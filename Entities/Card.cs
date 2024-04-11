﻿namespace trello_services.Entities
{
    public class Card
    {
        public Int64 cardId { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string? cover { get; set; }
        public Int64 columnId { get; set; }
        public Column Column { get; set; }
        public IList<CheckList> CheckLists { get; set; }
        public IList<UserCard> UserCards { get; set; }
        public IList<Activity> Activities { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<CardLabel> CardLabels { get; set; }
        public Card()
        {
            CheckLists = new List<CheckList>(); 
            UserCards = new List<UserCard>();
            Activities = new List<Activity>();
            Comments = new List<Comment>();
            CardLabels = new List<CardLabel>();
        }

    }
}
