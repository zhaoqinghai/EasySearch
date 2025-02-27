namespace EasySearchCore
{
    public class AppSetting
    {
        public DocParser DocParser { get; set; } = new DocParser();
    }

    public class DocParser
    {
        public int MaxTextLength { get; set; } = 2000000;

        public int MaxParseDocCount { get; set; } = 2000;

        public bool Enabled { get; set; } = true;
    }
}