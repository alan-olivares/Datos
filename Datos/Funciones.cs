using System;
using System.Globalization;
using System.Diagnostics;

namespace Datos
{
    public class Funciones
    {
        public static int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
        public static DateTime dayOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var weekNum = weekOfYear;
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
        public static String dayOfWeek(String fecha)
        {
            int year = getYear(fecha);
            int semana = getWeek(fecha);
            return dayOfWeek(year, semana).ToString("yyyy-MM-dd");
        }
        public static int getWeek(String fecha) {
            int semana = Int32.Parse(fecha.Substring(fecha.IndexOf('W') + 1));
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour < 7 && GetWeekNumber() == semana)
                semana--;
            return semana;
        }
        public static int getYear(String fecha){
            return Int32.Parse(fecha.Substring(0, 4));
        }
    }
}