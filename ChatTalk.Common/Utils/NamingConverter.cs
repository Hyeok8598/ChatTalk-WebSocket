using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatTalk.Common.Utils
{
    public static class NamingConverter
    {
        public static string ToSnakeCase(string text)
        {
            return Regex.Replace(text, "(?<!^)([A-Z])", "_$1").ToLower();
        }
    }
}