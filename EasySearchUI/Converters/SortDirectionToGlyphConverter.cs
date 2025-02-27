using EasySearchUI.Pages;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySearchUI.Converters
{
    public class SortDirectionToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is SortDirection sortDirection)
            {
                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        return "\uE70E";

                    case SortDirection.Descending:
                        return "\uE70D";

                    case SortDirection.None:
                    default:
                        return "\uE8CB";
                }
            }
            return "\uE8CB";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}