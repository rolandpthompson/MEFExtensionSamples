namespace CoreApplication.Extensibility
{
    public interface ITextExtraction
    {
        string Version { get; }
        string Author { get;}
        string Description { get; }
        string ExtractedText(string sourceText);
    }
}
