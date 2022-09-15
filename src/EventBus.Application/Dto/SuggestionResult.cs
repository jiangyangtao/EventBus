namespace EventBus.Application.Dto
{
    public class SuggestionResult
    {
        public SuggestionResult()
        {
        }

        public SuggestionResult(string text, string value, string description = "")
        {
            Text = text;
            Value = value;
            Description = description;
        }

        public string Text { get; set; }

        public string Value { set; get; }

        public string Description { set; get; }
    }
}
