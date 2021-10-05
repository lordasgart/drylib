namespace DryLib
{
    public class DateTimeHelper
    {
        public static string GetDateTimeNowForFilesystem()
        {
            var now = DateTime.Now;
            return $"{now.Year}-{now.Month.ToString().PadLeft(2, '0')}-{now.Day.ToString().PadLeft(2, '0')}T{now.Hour.ToString().PadLeft(2, '0')}{now.Minute.ToString().PadLeft(2, '0')}{now.Second.ToString().PadLeft(2, '0')}";
        }

        public static string GetDateNowForFilesystem()
        {
            var now = DateTime.Now;
            return $"{now.Year}-{now.Month.ToString().PadLeft(2, '0')}-{now.Day.ToString().PadLeft(2, '0')}";
        }

        public static string GetDateNowForFilesystem(DateTime dateTime)
        {
            return $"{dateTime.Year}-{dateTime.Month.ToString().PadLeft(2, '0')}-{dateTime.Day.ToString().PadLeft(2, '0')}";
        }
    }
}
