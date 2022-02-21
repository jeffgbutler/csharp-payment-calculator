using System;
using System.Threading.Tasks;

namespace PaymentCalculator.Services
{
    public class CrashService
    {
        public void CrashIt()
        {
            // ends the app after a 2 second delay
            Task.Run(async delegate
            {
                await Task.Delay(2000);
                Environment.Exit(22);
            });
        }
    }
}
