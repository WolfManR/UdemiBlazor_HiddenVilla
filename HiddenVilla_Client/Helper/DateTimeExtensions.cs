using System.Globalization;

namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToFormatedString(this DateTime self) => self.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    }
}
