namespace VideoGamesDapper.Models;

public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty!;
    public string Publisher { get; set; } = string.Empty!;
    public string Developer { get; set; } = string.Empty!;
    public DateTime? ReleaseDate { get; set; }
}
