namespace Api.Extensions
{
    /// <summary>
    /// Extension helpers
    /// </summary>
    public static class DateTimeExtensions
    {
        public static int Age(this DateTime birthDate) => Age(birthDate,DateTime.Now);
        public static int Age(this DateTime birthDate,DateTime when) => Convert.ToInt32(Math.Floor((when - birthDate).TotalDays / 365.25));
    }
}