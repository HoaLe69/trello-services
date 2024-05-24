namespace trello_services.Helpers
{
    public static class ValidGuid
    {
        public static bool IsValidGuid(string id)
        {
            Guid guid;
            return Guid.TryParse(id , out guid);
        }
    }
}
