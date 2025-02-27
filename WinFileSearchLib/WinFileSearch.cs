using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;

namespace WinFileSearchLib
{
    public class WinFileSearchApi
    {
        static WinFileSearchApi()
        {
            CheckEnv();
        }

        private static void CheckEnv()
        {
            if (Process.GetProcessesByName("qseverything").Length == 0)
            {
                var processInfo = new ProcessStartInfo()
                {
                    Arguments = @"-startup -admin -first-instance -instance ""qseverything""",
                    UseShellExecute = false,
                    FileName = "qseverything.exe",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(processInfo);
                NativeInvoker.Everything_UpdateAllFolderIndexes();
            }
        }

        public void SetQuery(SearchRequest sr)
        {
        }

        public IEnumerable<FileSystemInfo> GetFileResult(SearchRequest sr)
        {
            CheckEnv();
            if (NativeInvoker.Everything_GetSort() != sr.SortType)
            {
                NativeInvoker.Everything_SetSort(sr.SortType);
            }
            NativeInvoker.Everything_SetRequestFlags(FileFieldFlag.EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME);
            NativeInvoker.Everything_SetOffset(sr.PageIndex * sr.PageSize);
            NativeInvoker.Everything_SetMax(sr.PageSize);
            if (!string.IsNullOrEmpty(sr.Keyword))
            {
                NativeInvoker.Everything_SetSearch(sr.Keyword);
            }
            else
            {
                NativeInvoker.Everything_SetSearch(string.Empty);
            }

            if (!NativeInvoker.Everything_Query(true))
                yield break;

            sr.TotalCount = NativeInvoker.Everything_GetNumResults();
            const uint bufferSize = 260;
            StringBuilder buffer = new StringBuilder((int)bufferSize);
            for (uint i = 0; i < Math.Min(sr.TotalCount, sr.PageSize); i++)
            {
                if (NativeInvoker.Everything_GetResultFullPathName(i, buffer, bufferSize) != 0)
                {
                    var filePath = buffer.ToString();
                    if (File.Exists(filePath))
                    {
                        yield return new FileInfo(filePath);
                    }
                    else if (Directory.Exists(filePath))
                    {
                        yield return new DirectoryInfo(filePath);
                    }
                }
            }
        }
    }
}