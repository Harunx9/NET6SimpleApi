namespace SimpleApi.Model;

public interface ITime
{
    DateTime Now();

    DateTime NowUtc();
}

public class TimeWrapper : ITime
{
    public DateTime Now() => DateTime.Now;

    public DateTime NowUtc() => DateTime.UtcNow;
}