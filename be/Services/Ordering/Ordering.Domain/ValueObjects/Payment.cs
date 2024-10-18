namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string? CardName { get; init; } = default!;
    public string CardNumber { get; init; } = default!;
    public string Expiration { get; init; } = default!;
    public string Cvv { get; init; } = default!;
    public int PaymentMethod { get; init; } = default!;
}