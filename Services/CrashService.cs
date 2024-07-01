namespace Services;

public static class CrashService
{
    public static void CrashIt()
    {
        // ends the app after a 2-second delay
        Task.Run(async delegate
        {
            await Task.Delay(2000);
            Environment.Exit(22);
        });
    }
}
