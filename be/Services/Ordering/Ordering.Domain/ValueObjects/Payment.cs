namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string? CardName { get; init; } = default!;
    public string CardNumber { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string Cvv { get; init; } = default!;
    public int PaymentMethod { get; init; } = default!;
    
    protected Payment(){}
    
    public Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }
    
    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration, nameof(expiration));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3, nameof(cvv));
        
        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}