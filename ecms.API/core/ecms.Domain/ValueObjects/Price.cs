using SharedKernal;

namespace ecms.Domain.ValueObjects;

public sealed record Price
{
    public double Amount { get; set; }
    public Currency Currency { get; set; }

    public Price(double amount, Currency currency)
    {
        Ensure.GreaterThanZero(amount);
        Amount = amount;
        Currency = currency;
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}

public enum Currency
{
    Pln = 1,
    Usd = 2,
    Eur = 3,
    Gbp = 4,
}