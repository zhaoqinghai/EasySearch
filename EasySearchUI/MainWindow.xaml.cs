using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.Graphics;
using Windows.UI.ViewManagement;
using CommunityToolkit.Mvvm.ComponentModel;
using EasySearchUI.Pages;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Media.Animation;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EasySearchUI.Messages;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EasySearchUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    [ObservableObject]
    public sealed partial class MainWindow : Window
    {
        private const int MinHeight = 480;
        private const int MinWidth = 720;

        public MainWindow()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
            AppWindow.TitleBar.BackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.IconShowOptions = IconShowOptions.ShowIconAndSystemMenu;
            Init();
            InitializeComponent();
            AppWindow.Changed += AppWindow_Changed;
        }

        private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
        {
            if (args.DidSizeChange)
            {
                if (AppWindow.Size.Height < MinHeight && AppWindow.Size.Width < MinWidth)
                    AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = MinWidth, Height = MinHeight });
                else if (AppWindow.Size.Height < MinHeight)
                    AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = AppWindow.Size.Width, Height = MinHeight });
                else if (AppWindow.Size.Width < MinWidth)
                    AppWindow.Resize(new Windows.Graphics.SizeInt32 { Width = MinWidth, Height = AppWindow.Size.Height });
                SetRegionsForCustomTitleBar();
            }
        }

        private void SetRegionsForCustomTitleBar()
        {
            GeneralTransform transform = TitleSearchBox.TransformToVisual(null);
            Rect bounds = transform.TransformBounds(new Rect(0, 0, TitleSearchBox.ActualWidth, TitleSearchBox.ActualHeight));
            RectInt32 rect = new RectInt32
            {
                X = (int)bounds.X,
                Y = (int)bounds.Y,
                Width = (int)bounds.Width,
                Height = (int)bounds.Height
            };

            var rectArray = new RectInt32[] { rect };

            InputNonClientPointerSource nonClientInputSrc =
                InputNonClientPointerSource.GetForWindowId(this.AppWindow.Id);
            nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rectArray);
        }

        [ObservableProperty]
        private string? _searchText;

        partial void OnSearchTextChanged(string? value)
        {
            Task.Factory.StartNew(() =>
            {
                WeakReferenceMessenger.Default.Send(new SearchTextChangeMessage(value));
            });
        }

        private RectInt32[] GetRemainingRectangles(int width, int height, int cutWidth, int cutHeight)
        {
            var rectangles = new RectInt32[4];

            if (height > cutHeight)
                rectangles[0] = new RectInt32(0, 0, width, (height - cutHeight) / 2);

            if (height > cutHeight)
                rectangles[1] = new RectInt32(0, (height + cutHeight) / 2, width, (height - cutHeight) / 2);

            if (width > cutWidth)
                rectangles[2] = new RectInt32(0, (height - cutHeight) / 2, (width - cutWidth) / 2, cutHeight);

            if (width > cutWidth)
                rectangles[3] = new RectInt32((width + cutWidth) / 2, (height - cutHeight) / 2, (width - cutWidth) / 2, cutHeight);

            return rectangles;
        }

        private void Init()
        {
            AppWindow.Resize(new SizeInt32()
            {
                Height = 640,
                Width = 960
            });
            AppWindow.TitleBar.SetDragRectangles(GetRemainingRectangles(AppWindow.Size.Width, AppWindow.TitleBar.Height, 360, 32));
            if (DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Nearest) is DisplayArea displayArea)
            {
                var CenteredPosition = AppWindow.Position;
                CenteredPosition.X = (displayArea.WorkArea.Width - AppWindow.Size.Width) / 2;
                CenteredPosition.Y = (displayArea.WorkArea.Height - AppWindow.Size.Height) / 2;
                AppWindow.Move(CenteredPosition);
            }
        }
    }
}