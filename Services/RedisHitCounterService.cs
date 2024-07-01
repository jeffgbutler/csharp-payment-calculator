using Microsoft.Extensions.Caching.Distributed;

namespace Services;

public class RedisHitCounterService(IDistributedCache cache) : IHitCounterService
{
    private const long DefaultValue = 5000;
    private const string Key = "theKey";

    public long GetAndIncrement()
    {
        var count = GetCount();
        SetCount(count + 1);
        return count;
    }

    public void Reset()
    {
        SetCount(DefaultValue);
    }

    private long GetCount()
    {
        var value = cache.GetString(Key);

        return value == null ? DefaultValue : long.Parse(value);
    }

    private void SetCount(long count)
    {
        cache.SetString(Key, count.ToString());
    }
}
