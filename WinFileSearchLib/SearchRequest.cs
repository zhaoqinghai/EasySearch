using System;

namespace WinFileSearchLib
{
    public class SearchRequest
    {
        private SearchRequest()
        { }

        public static SearchRequest Create() => new SearchRequest();

        public string Keyword { get; private set; }

        public SortType SortType { get; private set; } = SortType.EVERYTHING_SORT_DATE_MODIFIED_DESCENDING;

        public TimeSpan Timeout { get; private set; } = TimeSpan.FromMilliseconds(5000);

        public uint PageSize { get; private set; } = 20;

        /// <summary>
        /// 从0开始
        /// </summary>
        public uint PageIndex { get; private set; } = 0;

        public uint TotalCount { get; internal set; }

        public SearchRequest WithKeyword(string keyword)
        {
            this.Keyword = keyword;
            return this;
        }

        public SearchRequest WithSortType(SortType sortType)
        {
            this.SortType = sortType;
            return this;
        }

        public SearchRequest WithTimeout(TimeSpan timeout)
        {
            this.Timeout = timeout;
            return this;
        }

        public SearchRequest WithPageSetting(uint pageIndex = 0, uint pageSize = 20)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }
    }
}