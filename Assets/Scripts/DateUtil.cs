using System;

public class DateUtil
{
    public static int GetEpochTime ()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        return (int)t.TotalSeconds;
    }
}
