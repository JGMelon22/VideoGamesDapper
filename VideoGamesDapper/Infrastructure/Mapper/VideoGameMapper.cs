using Riok.Mapperly.Abstractions;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Infrastructure.Mapper;

[Mapper]
public partial class VideoGameMapper
{
    public partial VideoGameResponse VideoGameToVideoGameResult(VideoGame videoGame);
    public partial VideoGame VideoGameInputToVideoGame(VideoGameInput videoGame);
}
