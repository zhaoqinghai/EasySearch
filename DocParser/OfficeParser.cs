using ikvm.io;
using java.io;
using org.apache.tika.config;
using org.apache.tika.metadata;
using org.apache.tika.parser;
using org.apache.tika.parser.microsoft.ooxml;
using org.apache.tika.sax;
using System;
using System.IO;

namespace DocParser
{
    [IncludeExt("doc|docx|xls|xlsx|ppt|pptx")]
    public class OfficeParser : ParserBase
    {
        public OfficeParser(ParserBuilder builder) : base(builder)
        {
        }

        public override Stream Parse()
        {
            var outputFileName = GetTempFilePath();
            using var inputFs = new FileStream(_builder._filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using InputStreamWrapper stream = new InputStreamWrapper(inputFs);
            var outputFs = new FileOutputStream(outputFileName);
            try
            {
                BodyContentHandler handler = new BodyContentHandler(outputFs);
                Metadata metadata = new Metadata();
                ParseContext context = new ParseContext();
                new AutoDetectParser(new TikaConfig("tika-config.xml")).parse(stream, handler, metadata, context);
                if (new FileInfo(outputFileName).Length == 0)
                {
                    return Stream.Null;
                }
                else
                {
                    return new AutoDeleteFileStream(outputFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
            }
            finally
            {
                outputFs.close();
            }
        }
    }
}