using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using System.Drawing;
using System.Drawing.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.InteropServices;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace EasySearchUI.Converters
{
    public class FileIconConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string filePath && File.Exists(filePath))
            {
                var icon = GetFileIcon(filePath);
                return icon;
            }
            else
            {
                return FolderImg.Default.Image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private BitmapImage? GetFileIcon(string filePath)
        {
            using (var memoryStream = new MemoryStream())
            {
                var icon = Icon.ExtractAssociatedIcon(filePath);
                if (icon == null)
                {
                    return null;
                }
                icon.ToBitmap().Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmapImage = new BitmapImage();
                var stream = memoryStream.AsRandomAccessStream();
                stream.Seek(0);
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
        }
    }

    public class FolderImg
    {
        private static Lazy<FolderImg> _default = new Lazy<FolderImg>(() => new FolderImg());

        public static FolderImg Default => _default.Value;

        private FolderImg()
        {
            Image = GetFolderIcon();
        }

        public BitmapImage? Image { get; }

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        const uint SHGFI_ICON = 0x000000100;
        const uint SHGFI_USEFILEATTRIBUTES = 0x00000010;
        const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;

        private BitmapImage? GetFolderIcon()
        {
            string folderPath = Environment.CurrentDirectory;
            SHFILEINFO fileInfo = new SHFILEINFO();

            SHGetFileInfo(folderPath, FILE_ATTRIBUTE_DIRECTORY, ref fileInfo, (uint)Marshal.SizeOf(typeof(SHFILEINFO)), SHGFI_ICON);

            if (fileInfo.hIcon != IntPtr.Zero)
            {
                return ConvertIconToBitmapImage(fileInfo.hIcon);
            }
            return null;
        }

        private BitmapImage ConvertIconToBitmapImage(IntPtr hIcon)
        {
            using (var ms = new MemoryStream())
            {
                Icon.FromHandle(hIcon).ToBitmap().Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmapImage = new BitmapImage();
                var stream = ms.AsRandomAccessStream();
                stream.Seek(0);
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
        }
    }
}