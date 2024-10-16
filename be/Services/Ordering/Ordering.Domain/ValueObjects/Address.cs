namespace Ordering.Domain.ValueObjects;

public class Address
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string? EmailAddress { get; init; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
}