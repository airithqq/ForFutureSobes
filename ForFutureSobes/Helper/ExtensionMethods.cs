using ForFutureSobes.DTOs;

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
        public static string BuildGeminiDetailedPrompt(TaskSummaryDTO task)
        {
            return $"""
        Analyze the following task and suggest improvements, and the answer must be in the same language as the subject, description, and title.

        Title: {task.Title}
        Description: {task.Description}
        Theme: {task.Theme}

        provide suggestions or enhancements.
        """;
        }
        public static string BuildGeminiShortPrompt(TaskSummaryDTO task)
        {
            return $"""
        Analyze the following task and give fast and clear answer, and the answer must be in the same language as the subject, description, and title.

        Title: {task.Title}
        Description: {task.Description}
        Theme: {task.Theme}

        give concise and short advice on this
        """;
        }

    }

}
