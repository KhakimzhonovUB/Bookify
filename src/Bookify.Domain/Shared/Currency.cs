namespace Bookify.Domain.Shared;

public record Currency
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    
    internal static readonly Currency None = new(string.Empty);
    
    public static readonly IReadOnlyCollection<Currency> All =
    [
        Usd,
        Eur
    ];
    
    public string Code { get; }

    private Currency(string code)
    {
        Code = code;
    }
    
    public static Currency FromCode(string code) =>
        All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency code is invalid");
}