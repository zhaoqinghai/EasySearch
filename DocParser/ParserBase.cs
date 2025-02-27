using System.IO;

namespace DocParser
{
    public abstract class ParserBase : IParser
    {
        protected readonly ParserBuilder _builder;

        internal ParserBase(ParserBuilder builder) => _builder = builder;

        public abstract Stream Parse();

        public string GetTempFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }
    }
}