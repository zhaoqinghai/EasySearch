using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using WinFileSearchLib;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;
using System.Threading;
using CommunityToolkit.Mvvm.Messaging;
using EasySearchUI.Messages;
using CommunityToolkit.WinUI.Collections;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EasySearchUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [ObservableObject]
    public sealed partial class EverythingExplorePage : Page, IRecipient<SearchTextChangeMessage>
    {
        private readonly FileInfoSource _fileInfoSource;
        private string? _preSearchText;

        public EverythingExplorePage()
        {
            SearchFiles = new IncrementalLoadingCollection<FileInfoSource, FileSystemInfo>(_fileInfoSource ??= new FileInfoSource(), 200);
            SearchFiles.OnStartLoading = () =>
            {
                IsLoading = true;
            };
            SearchFiles.OnEndLoading = () =>
            {
                IsLoading = false;
            };
            this.InitializeComponent();

            WeakReferenceMessenger.Default.Register(this);
        }

        public IncrementalLoadingCollection<FileInfoSource, FileSystemInfo> SearchFiles { get; }

        public void Receive(SearchTextChangeMessage message)
        {
            _preSearchText = message.Value;
            DispatcherQueue.TryEnqueue(async () =>
            {
                if (Visibility == Visibility.Visible)
                {
                    while (!string.Equals(_preSearchText, _fileInfoSource.SearchText))
                    {
                        _fileInfoSource.SearchText = _preSearchText;
                        IsLoading = true;
                        await SearchFiles.RefreshAsync().ConfigureAwait(false);
                    }
                }
            });
        }

        [RelayCommand]
        private void OpenFolder(object obj)
        {
            if (obj is MenuFlyoutItem mfi && mfi.DataContext is FileInfo file)
            {
                if (string.IsNullOrEmpty(file.DirectoryName))
                {
                    Process.Start("explorer.exe", file.FullName);
                }
                else
                {
                    Process.Start("explorer.exe", file.DirectoryName);
                }
            }
        }

        [RelayCommand]
        private void DeleteFile(object obj)
        {
            if (obj is MenuFlyoutItem mfi && mfi.DataContext is FileInfo file)
            {
                Process.Start("explorer.exe", file.FullName);
            }
        }

        [RelayCommand]
        private void OpenFile(DoubleTappedRoutedEventArgs args)
        {
            if (args.OriginalSource is FrameworkElement fe && fe.DataContext is FileInfo file)
            {
                Process.Start("explorer.exe", file.FullName);
            }
        }

        [ObservableProperty]
        SortDirection _lastWriteSort = SortDirection.None;

        [ObservableProperty]
        SortDirection _fileNameSort = SortDirection.None;

        [ObservableProperty]
        bool _isLoading = false;

        private async void SortByLastWriteTime(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FileNameSort = SortDirection.None;
            if (LastWriteSort == SortDirection.None)
            {
                LastWriteSort = SortDirection.Ascending;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_DATE_MODIFIED_ASCENDING;
            }
            else if (LastWriteSort == SortDirection.Ascending)
            {
                LastWriteSort = SortDirection.Descending;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_DATE_MODIFIED_DESCENDING;
            }
            else
            {
                LastWriteSort = SortDirection.None;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING;
            }
            IsLoading = true;
            await SearchFiles.RefreshAsync().ConfigureAwait(false);
        }

        private async void SortByFileName(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            LastWriteSort = SortDirection.None;
            if (FileNameSort == SortDirection.None)
            {
                FileNameSort = SortDirection.Ascending;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING;
            }
            else if (FileNameSort == SortDirection.Ascending)
            {
                FileNameSort = SortDirection.Descending;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING;
            }
            else
            {
                FileNameSort = SortDirection.None;
                _fileInfoSource.SortDirection = SortType.EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING;
            }
            IsLoading = true;
            await SearchFiles.RefreshAsync().ConfigureAwait(false);
        }
    }

    public enum SortDirection
    {
        None,
        Ascending,
        Descending
    }

    public class FileInfoSource : CommunityToolkit.WinUI.Collections.IIncrementalSource<FileSystemInfo>
    {
        public string? SearchText { get; set; }

        public SortType SortDirection { get; set; }

        public async Task<IEnumerable<FileSystemInfo>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var searchRequest = SearchRequest.Create()
               .WithKeyword(SearchText)
               .WithSortType(SortDirection)
               .WithPageSetting((uint)pageIndex, (uint)pageSize);
                await Task.Delay(10);
                return new WinFileSearchApi().GetFileResult(searchRequest);
            }
            catch
            {
                return Enumerable.Empty<FileInfo>();
            }
        }
    }
}