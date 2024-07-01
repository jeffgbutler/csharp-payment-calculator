namespace Services;

public static class PaymentService
{
    public static decimal Calculate(double amount, double rate, int years)
    {
        return rate == 0.0 ? CalculateWithoutInterest(amount, years) : CalculateWithInterest(amount, rate, years);
    }

    private static decimal CalculateWithInterest(double amount, double rate, int years)
    {
        var monthlyRate = rate / 100.0 / 12.0;
        var numberOfPayments = years * 12;
        var payment = (monthlyRate * amount) / (1.0 - Math.Pow(1.0 + monthlyRate,
            -numberOfPayments));
        return ToMoney(payment);
    }

    private static decimal CalculateWithoutInterest(double amount, int years)
    {
        var numberOfPayments = years * 12;
        return ToMoney(amount / numberOfPayments);
    }

    private static decimal ToMoney(double d)
    {
        var bd = new decimal(d);
        return decimal.Round(bd, 2, MidpointRounding.AwayFromZero);
    }
}
