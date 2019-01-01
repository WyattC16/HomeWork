using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HomeWork.Web
{
    public static class Extensions
    {
        public static string Join(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);

        public static IEnumerable<string> SplitByUpperCase(this string str) =>
            Regex.Split(str, @"(?<!^)(?=[A-Z])");

        public static string ToProper(this string str) =>
            new CultureInfo("en-US", false).TextInfo.ToTitleCase(str.ToLower());

        public static IEnumerable<T> GetEnumArray<T>(this T enumVal) where T : Enum => 
            (T[]) Enum.GetValues(typeof(T));
    }
}