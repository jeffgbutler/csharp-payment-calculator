namespace Services;

public interface IHitCounterService
{
    long GetAndIncrement();
    void Reset();
}
