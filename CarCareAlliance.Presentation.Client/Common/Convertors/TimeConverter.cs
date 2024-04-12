namespace CarCareAlliance.Presentation.Client.Common.Convertors
{
    public static class TimeConverter
    {
        public static TimeSpan TimeOnlyToTimeSpan(TimeOnly time)
        {
            return new TimeSpan(time.Hour, time.Minute, time.Second);
        }

        public static TimeOnly TimeSpanToTimeOnly(TimeSpan time)
        {
            return new TimeOnly(time.Hours, time.Minutes, time.Seconds);
        }
    }

}
