namespace Services
{
    public class MemoryHitCounterService: IHitCounterService
    {
        private long HitCount = 0;
        public long GetAndIncrement()
        {
            return ++HitCount;
        }

        public void Reset()
        {
            HitCount = 0;
        }
    }
}
