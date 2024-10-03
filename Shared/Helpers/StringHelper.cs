namespace Shared.Helpers
{
    public static class StringHelper
    {
        public static string ReplaceFractionsWithDecimals(string input)
        {
            return input
                .Replace("¼", ".25")
                .Replace("½", ".5")
                .Replace("¾", ".75");
        }

        public static string FormatName(string input)
        {
            return input
                .Replace("amp;", "")
                .Replace("#39", "")
                .Replace("2fav", "")
                .Replace("j2fav", "")
                .Replace("fav", "");


        }
    }
}
