using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kalamarket.Core.Convartor
{
    public static class DateConvartor
    {
        public static string Toshamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            var Toshamsi = pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
           pc.GetDayOfMonth(value).ToString("00");
            return Toshamsi;
        }
    }
	public static class StringExtensions
	{
		public static string SkipImgTags(this string html, int length)
		{
			string strWithoutImgTags = Regex.Replace(html, @"(<img\/?[^>]+>)", @"", RegexOptions.IgnoreCase);

			return strWithoutImgTags.Substring(strWithoutImgTags.Length - length);
		}
	}
}
