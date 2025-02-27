using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySearchUI.Converters
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FileInfo file)
            {
                if (file.Length >= 1 << 30)
                    return $"{file.Length / (1 << 30)} GB";
                if (file.Length >= 1 << 20)
                    return $"{file.Length / (1 << 20)} MB";
                if (file.Length >= 1 << 10)
                    return $"{file.Length / (1 << 10)} KB";
                return $"{file.Length} B";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}