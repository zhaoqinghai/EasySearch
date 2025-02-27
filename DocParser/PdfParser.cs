using PdfiumWrapper;
using System.IO;

namespace DocParser
{
    [IncludeExt("pdf")]
    public class PdfParser : ParserBase
    {
        public PdfParser(ParserBuilder builder) : base(builder)
        {
        }

        public override Stream Parse()
        {
            using var pdfExecutor = new PdfExecutor(_builder._filePath);
            if (pdfExecutor.IsInValid())
            {
                return Stream.Null;
            }
            var fs = new AutoDeleteFileStream(GetTempFilePath(), FileMode.Open, FileAccess.Read);
            using var sw = new StreamWriter(fs);
            var totalCount = pdfExecutor.GetTotalPageCount();
            for (var i = 0; i < totalCount; i++)
            {
                var text = pdfExecutor.GetPageText(i);
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }
                sw.WriteLine(text);
            }
            return fs;
        }
    }
}