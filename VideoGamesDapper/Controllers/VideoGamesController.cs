using Microsoft.AspNetCore.Mvc;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;

namespace VideoGamesDapper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly IVideoGameRepository _videoGameRepository;

    public VideoGamesController(IVideoGameRepository videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddVideoGameAsync(VideoGameInput newVideoGame)
    {
        var videoGames = await _videoGameRepository.AddVideoGameAsync(newVideoGame);
        return videoGames.Data != 0
            ? Ok(videoGames)
            : BadRequest(videoGames);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateVideoGameAsync(int id, VideoGameInput newVideoGame)
    {
        var videoGame = await _videoGameRepository.UpdateVideoGameAsync(id, newVideoGame);
        return videoGame.Data != 0
            ? Ok(videoGame)
            : BadRequest(videoGame);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVideoGamesAsync()
    {
        var videoGames = await _videoGameRepository.GetAllVideoGamesAsync();
        return videoGames != null && videoGames.Data!.Any()
            ? Ok(videoGames)
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVideoGameByIdAsync(int id)
    {
        var videoGame = await _videoGameRepository.GetVideoGameByIdAsync(id);
        return videoGame != null
            ? Ok(videoGame)
            : NotFound(videoGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveVideoGameAsync(int id)
    {
        var videoGame = await _videoGameRepository.RemoveVideoGameAsync(id);
        return videoGame.Data != 0
            ? NoContent()
            : NotFound(videoGame);
    }
}
