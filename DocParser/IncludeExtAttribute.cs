using System;

namespace DocParser
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IncludeExtAttribute : Attribute
    {
        /// <summary>
        /// 包含的文件扩展名
        /// </summary>
        /// <param name="exts">xls|xlsx</param>
        public IncludeExtAttribute(string exts)
        { }
    }
}