namespace DocParser
{
    public partial class ParserBuilder
    {
        internal readonly string _filePath;

        public ParserBuilder(string filePath)
        {
            _filePath = filePath;
        }

        public partial IParser Build();
    }
}