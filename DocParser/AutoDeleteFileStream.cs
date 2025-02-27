using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Threading.Tasks;

namespace DocParser
{
    public class AutoDeleteFileStream : FileStream
    {
        public AutoDeleteFileStream(SafeFileHandle handle, FileAccess access) : base(handle, access)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode) : base(path, mode)
        {
        }

        public AutoDeleteFileStream(string path, FileStreamOptions options) : base(path, options)
        {
        }

        public AutoDeleteFileStream(SafeFileHandle handle, FileAccess access, int bufferSize) : base(handle, access, bufferSize)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access) : base(path, mode, access)
        {
        }

        public AutoDeleteFileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) : base(handle, access, bufferSize, isAsync)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access, FileShare share) : base(path, mode, access, share)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : base(path, mode, access, share, bufferSize)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : base(path, mode, access, share, bufferSize, useAsync)
        {
        }

        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options) : base(path, mode, access, share, bufferSize, options)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (File.Exists(Name))
            {
                File.Delete(Name);
            }
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            if (File.Exists(Name))
            {
                File.Delete(Name);
            }
        }
    }
}