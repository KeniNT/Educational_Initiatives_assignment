using System;

namespace SmartOffice.Infrastructure
{
    public static class Utils
    {
        public static bool TryParseTime(string input, out DateTime time)
        {
            // Accepts HH:mm (24-hour)
            return DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out time);
        }

        public static bool IsPositiveInt(string s, out int val)
        {
            return int.TryParse(s, out val) && val > 0;
        }
    }
}
