using System.Globalization;
using System.Reflection;

namespace Utilities.DateUtils;
public static class DateConvertor
{
    //6/16/2025 12:00:00 AM
    public static string ToPersianDate(string? date)
    {
        try
        {
            if (string.IsNullOrEmpty(date)) return "";
            PersianCalendar persianCalendar = new PersianCalendar();
            string[] dates = date.Split(' ');
            string[] strings = dates[0].Split('/');
            DateTime englisgDate = new DateTime(Convert.ToInt32(strings[2]), Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]));
            return $"{persianCalendar.GetYear(englisgDate)}/{persianCalendar.GetMonth(englisgDate)}/{persianCalendar.GetDayOfMonth(englisgDate)}";
        }
        catch 
        {
            return "";
        }
     }
    public static string ToPersianDate(DateTime? englisgDate)
    {
        try
        {
            if (englisgDate == null) return "";
            PersianCalendar persianCalendar = new PersianCalendar();
            return $"{persianCalendar.GetYear(englisgDate.Value)}/{persianCalendar.GetMonth(englisgDate.Value)}/{persianCalendar.GetDayOfMonth(englisgDate.Value)}";
        }
        catch 
        {
            return "";
        }
    }
    public static DateTime? ToEnglishDateTime(string? date)
    {
        try
        {
            if (string.IsNullOrEmpty(date)) return null;
            string[] strings = date.Split(':');
            if (strings.Length == 3)
                return new DateTime(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]), Convert.ToInt32(strings[2]), new PersianCalendar());
            else return null;
        }
        catch 
        {
            return null;
        }
    }
}
