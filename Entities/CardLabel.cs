﻿namespace trello_services.Entities
{
    public class CardLabel
    {
        public Int64 cardId { get; set; }
        public Guid  labelId { get; set; }
        public Label Label { get; set;}
        public Card Card { get; set;}
    }
}
