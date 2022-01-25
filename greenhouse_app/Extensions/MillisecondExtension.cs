using System;
namespace greenhouse_app.Extensions
{
    public static class MillisecondExtension
    {
        public static Task Millisecond(this int millisecond)
        {
            return Task.Delay(new TimeSpan(0, 0, 0, millisecond));
        }
    }
}

