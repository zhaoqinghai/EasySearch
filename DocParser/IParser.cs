using System.IO;

namespace DocParser
{
    public interface IParser
    {
        Stream Parse();
    }
}