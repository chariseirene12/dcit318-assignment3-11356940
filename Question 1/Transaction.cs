public record Transaction
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public string Category { get; init; } = string.Empty;
}
