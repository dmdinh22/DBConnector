using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DBConnector.Tools
{
    public static class DataHelper
    {
        public static T Get<T>(this DataRow dr, int index, T defaultValue = default(T)) { return dr[index].Get<T>(defaultValue); }

        public static T Get<T>(this DataRow dr, string column_name, T defaultValue = default(T)) { return dr[column_name].Get<T>(defaultValue); }

        static T Get<T>(this object obj, T defaultValue) //private method on object.. just to use internally
        {
            if (obj.IsNull())
                return defaultValue;

            if (String.IsNullOrEmpty(obj.ToString().Trim())) { return default(T); }
            return (T)obj;
        }

        public static bool IsNull<T>(this T obj) where T : class
        {
            return (object)obj == null || obj == DBNull.Value;
        }

        public static T Get<T>(this IDataReader dr, int index, T defaultValue = default(T))
        {
            return dr[index].Get<T>(defaultValue);
        }

        public static T Get<T>(this IDataReader dr, string column_name, T defaultValue = default(T))
        {
            return dr[column_name].Get<T>(defaultValue);
        }

        public static double GetDouble(this DataRow dr, string column_name)
        {
            double dbl = 0;
            double.TryParse(dr[column_name].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this DataRow dr, int column_index)
        {
            double dbl = 0;
            double.TryParse(dr[column_index].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this IDataReader dr, string column_name)
        {
            double dbl = 0;
            double.TryParse(dr[column_name].ToString(), out dbl);
            return dbl;
        }

        public static double GetDouble(this IDataReader dr, int column_index)
        {
            double dbl = 0;
            double.TryParse(dr[column_index].ToString(), out dbl);
            return dbl;
        }

        public static string CleanDbString(this string input)
        {
            if (String.IsNullOrEmpty(input)) { return input; }
            input = input.Replace("'", "''");
            input = input.Replace("--", "- -");
            input = input.Replace("sp_", "s p _");
            input = input.Trim();
            return input;
        }

        public static string AsReversedString(this string input_value)
        {
            if (string.IsNullOrEmpty(input_value)) { return input_value; }
            char[] chars = input_value.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        public static int CalculatePageTotals(this int total_rows, int rows_per_page)
        {
            if (total_rows <= rows_per_page) { return 1; }
            int pgs = total_rows / rows_per_page;
            int mod = total_rows % rows_per_page;
            pgs += (mod >= 1 ? 1 : 0);
            return pgs;
        }

        public static DateTime ValidateDate(this string date_time)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(date_time, out dt);
            return dt;
        }

        public static int ToInt32(this Object obj)
        {
            if (obj == null) return 0;
            int val = 0;
            int.TryParse(obj.ToString(), out val);
            return val;
        }

        public static double MakeDouble(this string str)
        {
            double val = 0;
            double.TryParse(str, out val);
            return val;
        }

        public static int DaysFrom(this DateTime start_date, DateTime end_date)
        {
            TimeSpan ts = (end_date - start_date);
            return ts.Days;
        }

        public static int MonthsFrom(this DateTime start_date, DateTime end_date)
        {
            if (start_date > end_date)
                return 0;

            int months = ((end_date.Year * 12) + end_date.Month) - ((start_date.Year * 12) + start_date.Month);
            if (end_date.Day >= start_date.Day)
                months++;

            return months;
        }

        public static string TrimIt(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            return string.Empty;
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;
                if (count != size)
                    continue;

                yield return bucket.Select(x => x);
                bucket = null;
                count = 0;
            }
            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
                yield return bucket.Take(count);
        }

        public static int RandomNumber(this int min_number, int max_number)
        {
            Random rnd = new Random();
            return rnd.Next(min_number, max_number);
        }
    }
}