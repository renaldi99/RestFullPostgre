namespace RestFullPostgre.Helpers
{
    public class Utility
    {
        public static string DecodeSpecialCharacters(string value)
        {
            return System.Web.HttpUtility.HtmlDecode(value);
        }

        public static string EncodeAmpersand(string value)
        {
            return value.Replace("&", "&amp;");
        }


    }
}
