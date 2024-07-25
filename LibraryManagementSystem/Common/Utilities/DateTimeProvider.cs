namespace LibraryManagementSystem;

public class DateTimeProvider
{
    private static Func<DateTime> _now = () => DateTime.Now;

    public static DateTime Now => _now();

    public static void SetDateTime(DateTime dateTime)
    {
        _now = () => dateTime;
    }

    public static void Reset()
    {
        _now = () => DateTime.Now;
    }
}

