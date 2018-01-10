namespace GWiLi.EntityFramework.Extensions
{
    public static class StringExtensions
    {
        public static string AddString(this string s, string addedString, bool shouldAddSpace = false)
        {
            return s + (shouldAddSpace ? " " : string.Empty) + addedString;
        }
    }
}