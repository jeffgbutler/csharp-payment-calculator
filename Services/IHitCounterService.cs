using System;

namespace PaymentCalculator.Services
{
    public interface IHitCounterService
    {
        long GetAndIncrement();
        void Reset();
    }
}
