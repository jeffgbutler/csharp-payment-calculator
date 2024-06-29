namespace Services;

public class MemoryHitCounterService: IHitCounterService
{
    private long hitCount;
    
    public long GetAndIncrement()
    {
        return ++hitCount;
    }

    public void Reset()
    {
        hitCount = 0;
    }
}
