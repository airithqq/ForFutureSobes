namespace ForFutureSobes.DTOs
{
    public class GeminiDTOs
    {
        public class GenerateTextRequest
        {
            public string Prompt { get; set; } = string.Empty;
        }

        public class GeminiResponse
        {
            public Candidate[] Candidates { get; set; } = Array.Empty<Candidate>();
        }

        public class Candidate
        {
            public Content Content { get; set; }
        }

        public class Content
        {
            public Part[] Parts { get; set; } = Array.Empty<Part>();
        }

        public class Part
        {
            public string Text { get; set; }
        }

    }
}
