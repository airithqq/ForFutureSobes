using System.Text;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Helper
{
    public static class PromptBuilder
    {
        public static string BuildGeminiPrompt(TaskSummaryDTO task)
        {
            return $"""
        Analyze the following task and suggest improvements:

        Title: {task.Title}
        Description: {task.Description}
        Theme: {task.Theme}

        provide suggestions or enhancements.
        """;
        }
        public static string BuildGeminiPrompt(List<TaskSummaryDTO> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Analyze the following tasks and suggest improvements grouped by theme:\n");

            int index = 1;
            foreach (var task in tasks)
            {
                sb.AppendLine($"Task {index++}:");
                sb.AppendLine($"Title: {task.Title}");
                sb.AppendLine($"Description: {task.Description}");
                sb.AppendLine($"Theme: {task.Theme}");
                sb.AppendLine();
            }

            sb.AppendLine("Please provide insights or enhancements for each theme.");
            return sb.ToString();
        }

    }
}
