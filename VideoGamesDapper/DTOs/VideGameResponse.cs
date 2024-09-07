namespace VideoGamesDapper.DTOs;

public record VideoGameResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty!;
    public string Publisher { get; init; } = string.Empty!;
    public string Developer { get; init; } = string.Empty!;
    public DateTime? ReleaseDate { get; init; }
}
