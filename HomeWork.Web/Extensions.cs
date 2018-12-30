using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HomeWork.Web
{
    public static class Extensions
    {
        public static IEnumerable<string> SplitByUpperCase(this string str) =>
            Regex.Split(str, @"(?<!^)(?=[A-Z])");

        public static string ToProper(this string str) =>
            new CultureInfo("en-US", false).TextInfo.ToTitleCase(str.ToLower());
    }
}