namespace RestFullPostgre.Helpers
{
    public static class Utility
    {
        public static string DecodeSpecialCharacters(string value)
        {
            return System.Web.HttpUtility.HtmlDecode(value);
        }

        public static string EncodeAmpersand(string value)
        {
            return value.Replace("&", "&amp;");
        }

        public static IEnumerable<T> Pagination<T>(this IEnumerable<T> datas, int page, int size)
        {
            return datas.Skip((page - 1) * size).Take(size).ToList();
        }
    }
}
