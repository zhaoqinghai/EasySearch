using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WinFileSearchLib
{
    internal static class NativeInvoker
    {
        internal const int EVERYTHING_SDK_VERSION = 2;

#if x86
        private const string EVERY_THINEG_DLL = "Everything32.dll";
#else
        private const string EVERY_THINEG_DLL = "Everything64.dll";
#endif

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern void Everything_SetSearch(string lpString);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetRegex(bool bEnable);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetMax(uint dwMax);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetOffset(uint dwOffset);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetReplyWindow(IntPtr hWnd);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetReplyID(uint dwId);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetSort(SortType dwSort);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SetRequestFlags(FileFieldFlag dwRequestFlags);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_GetMatchPath();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_GetMatchCase();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_GetMatchWholeWord();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_GetRegex();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetMax();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetOffset();

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern string Everything_GetSearch();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern ErrCode Everything_GetLastError();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern IntPtr Everything_GetReplyWindow();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetReplyID();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern SortType Everything_GetSort();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern FileFieldFlag Everything_GetRequestFlags();

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern bool Everything_Query(bool bWait);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsQueryReply(uint message, IntPtr wParam, IntPtr lParam, uint dwId);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_SortResultsByPath();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetNumFileResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetNumFolderResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetNumResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetTotFileResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetTotFolderResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetTotResults();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsVolumeResult(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsFolderResult(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsFileResult(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileName(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultPath(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern uint Everything_GetResultFullPathNameW(uint dwIndex, StringBuilder wbuf, uint wbufSizeInWchars);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_Reset();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern void Everything_CleanUp();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetMajorVersion();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetMinorVersion();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetRevision();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetBuildNumber();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_Exit();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_MSIExitAndStopService(IntPtr msiHandle);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_MSIStartService(IntPtr msiHandle);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsDBLoaded();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsAdmin();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsAppData();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_RebuildDB();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_UpdateAllFolderIndexes();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_SaveDB();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_SaveRunHistory();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_DeleteRunHistory();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern EverythingTargetPlatform Everything_GetTargetMachine();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsFastSort(uint sortType);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern bool Everything_IsFileInfoIndexed(uint fileInfoType);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern uint Everything_GetRunCountFromFileName(string lpFileName);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern bool Everything_SetRunCountFromFileName(string lpFileName, uint dwRunCount);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern uint Everything_IncRunCountFromFileName(string lpFileName);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern uint Everything_GetResultFullPathName(uint dwIndex, StringBuilder buf, uint bufsize);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetResultListSort();

        [DllImport(EVERY_THINEG_DLL)]
        public static extern FileFieldFlag Everything_GetResultListRequestFlags();

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultExtension(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultSize(uint dwIndex, out long lpSize);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultDateCreated(uint dwIndex, out FILETIME lpDateCreated);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultDateModified(uint dwIndex, out FILETIME lpDateModified);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultDateAccessed(uint dwIndex, out FILETIME lpDateAccessed);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetResultAttributes(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileListFileName(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL)]
        public static extern uint Everything_GetResultRunCount(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultDateRun(uint dwIndex, out FILETIME lpDateRun);

        [DllImport(EVERY_THINEG_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Everything_GetResultDateRecentlyChanged(uint dwIndex, out FILETIME lpDateRecentlyChanged);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFileName(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedPath(uint dwIndex);

        [DllImport(EVERY_THINEG_DLL, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFullPathAndFileName(uint dwIndex);

        // 将返回的 IntPtr 转换为字符串的辅助方法
        private static string PtrToString(IntPtr ptr)
        {
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStringUni(ptr);
        }

        public static string GetResultFileName(uint dwIndex) => PtrToString(Everything_GetResultFileName(dwIndex));

        public static string GetResultPath(uint dwIndex) => PtrToString(Everything_GetResultPath(dwIndex));

        public static string GetResultExtension(uint dwIndex) => PtrToString(Everything_GetResultExtension(dwIndex));

        public static string GetResultFileListFileName(uint dwIndex) => PtrToString(Everything_GetResultFileListFileName(dwIndex));

        public static string GetResultHighlightedFileName(uint dwIndex) => PtrToString(Everything_GetResultHighlightedFileName(dwIndex));

        public static string GetResultHighlightedPath(uint dwIndex) => PtrToString(Everything_GetResultHighlightedPath(dwIndex));

        public static string GetResultHighlightedFullPathAndFileName(uint dwIndex) => PtrToString(Everything_GetResultHighlightedFullPathAndFileName(dwIndex));
    }

    internal enum ErrCode : int
    {
        EVERYTHING_OK = 0,
        EVERYTHING_ERROR_MEMORY,
        EVERYTHING_ERROR_IPC,
        EVERYTHING_ERROR_REGISTERCLASSEX,
        EVERYTHING_ERROR_CREATEWINDOW,
        EVERYTHING_ERROR_CREATETHREAD,
        EVERYTHING_ERROR_INVALIDINDEX,
        EVERYTHING_ERROR_INVALIDCALL,
        EVERYTHING_ERROR_INVALIDREQUEST,
        EVERYTHING_ERROR_INVALIDPARAMETER
    }

    public enum SortType : uint
    {
        EVERYTHING_SORT_NAME_ASCENDING = 1,
        EVERYTHING_SORT_NAME_DESCENDING,
        EVERYTHING_SORT_PATH_ASCENDING,
        EVERYTHING_SORT_PATH_DESCENDING,
        EVERYTHING_SORT_SIZE_ASCENDING,
        EVERYTHING_SORT_SIZE_DESCENDING,
        EVERYTHING_SORT_EXTENSION_ASCENDING,
        EVERYTHING_SORT_EXTENSION_DESCENDING,
        EVERYTHING_SORT_TYPE_NAME_ASCENDING,
        EVERYTHING_SORT_TYPE_NAME_DESCENDING,
        EVERYTHING_SORT_DATE_CREATED_ASCENDING,
        EVERYTHING_SORT_DATE_CREATED_DESCENDING,
        EVERYTHING_SORT_DATE_MODIFIED_ASCENDING,
        EVERYTHING_SORT_DATE_MODIFIED_DESCENDING,
        EVERYTHING_SORT_ATTRIBUTES_ASCENDING,
        EVERYTHING_SORT_ATTRIBUTES_DESCENDING,
        EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING,
        EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING,
        EVERYTHING_SORT_RUN_COUNT_ASCENDING,
        EVERYTHING_SORT_RUN_COUNT_DESCENDING,
        EVERYTHING_SORT_DATE_RECENTLY_CHANGED_ASCENDING,
        EVERYTHING_SORT_DATE_RECENTLY_CHANGED_DESCENDING,
        EVERYTHING_SORT_DATE_ACCESSED_ASCENDING,
        EVERYTHING_SORT_DATE_ACCESSED_DESCENDING,
        EVERYTHING_SORT_DATE_RUN_ASCENDING,
        EVERYTHING_SORT_DATE_RUN_DESCENDING
    }

    [Flags]
    internal enum FileFieldFlag : uint
    {
        EVERYTHING_REQUEST_FILE_NAME = 1,
        EVERYTHING_REQUEST_PATH = 1 << 1,
        EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 1 << 2,
        EVERYTHING_REQUEST_EXTENSION = 1 << 3,
        EVERYTHING_REQUEST_SIZE = 1 << 4,
        EVERYTHING_REQUEST_DATE_CREATED = 1 << 5,
        EVERYTHING_REQUEST_DATE_MODIFIED = 1 << 6,
        EVERYTHING_REQUEST_DATE_ACCESSED = 1 << 7,
        EVERYTHING_REQUEST_ATTRIBUTES = 1 << 8,
        EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 1 << 9,
        EVERYTHING_REQUEST_RUN_COUNT = 1 << 10,
        EVERYTHING_REQUEST_DATE_RUN = 1 << 11,
        EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 1 << 12,
        EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 1 << 13,
        EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 1 << 14,
        EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 1 << 15
    }

    internal enum EverythingTargetPlatform
    {
        EVERYTHING_TARGET_MACHINE_X86 = 1,
        EVERYTHING_TARGET_MACHINE_X64,
        EVERYTHING_TARGET_MACHINE_ARM
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FILETIME
    {
        public uint dwLowDateTime;
        public uint dwHighDateTime;
    }
}