namespace VideoGamesDapper.DTOs;

public record VideoGameInput(
    string Title,
    string Publisher,
    string Developer,
    DateTime ReleaseDate
);
