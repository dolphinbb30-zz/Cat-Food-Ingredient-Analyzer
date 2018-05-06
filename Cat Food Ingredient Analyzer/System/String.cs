namespace System
{
    public static class String
    {
        public static bool CaseInsensitiveContains(this string text, string value, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
}