using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeConstructor.Properties;

namespace CodeConstructor.Support.Constructor
{
    /// <summary>
    /// 检查名称是否为关键字
    /// </summary>
    public class CheckKeywords
    {
        public static string InCSharp(string keyword)
        {
            if (IsCSharpKeyword(keyword))
            {
                return "_" + keyword.Replace("_", "");
            }
            return keyword;
        }

        public static bool IsCSharpKeyword(string keyword)
        {
            string[] keywords = Settings.Default.keywordsCSharp.Split(',');
            var _keywords =
                from addr in keywords
                where addr.EndsWith(keyword.ToLower())
                select addr;
            keywords = _keywords.ToArray<String>();
            if (keywords.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
