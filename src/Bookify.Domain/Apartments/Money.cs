namespace Bookify.Domain.Apartments;

public record Money(decimal Amount, Currency Currency)
{
    public static Money Zero => new(0, Currency.None);
    
    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Currencies have to be equal");
        
        return new Money(left.Amount + right.Amount, left.Currency);
    }
}
