namespace ForFutureSobes.Helper
{
    public static class ExtensionMethods
    {
        public static string FormatGeminiText(this string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
                return "No response received.";
            var htmlFormatted = rawText.Replace("\n", "<br/>");
            return $"<pre>{htmlFormatted}</pre>";
        }

    }

}
