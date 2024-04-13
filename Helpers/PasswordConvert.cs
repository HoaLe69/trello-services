namespace trello_services.Helpers
{
    public static class PasswordConvert
    {
        public static string EncryptPasswordBase64(string password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string DecryptPasswordBase64(string passwordEncrypted)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(passwordEncrypted);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
