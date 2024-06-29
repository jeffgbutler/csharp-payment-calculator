namespace Services;

public class MemoryHitCounterService: IHitCounterService
{
    private long _hitCount;
    
    public long GetAndIncrement()
    {
        return ++_hitCount;
    }

    public void Reset()
    {
        _hitCount = 0;
    }
}
