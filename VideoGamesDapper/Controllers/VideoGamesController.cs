using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Application.Queries;
using VideoGamesDapper.DTOs;

namespace VideoGamesDapper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoGamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVideoGamesAsync()
    {
        var videoGames = await _mediator.Send(new GetVideoGamesQuery());
        return videoGames != null && videoGames.Data!.Any()
            ? Ok(videoGames)
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVideoGameByIdAsync(int id)
    {
        var videoGame = await _mediator.Send(new GetVideoGameByIdQuery(id));
        return videoGame != null
            ? Ok(videoGame)
            : NotFound(videoGame);
    }

    [HttpPost]
    public async Task<IActionResult> AddVideoGameAsync(VideoGameInput newVideoGame)
    {
        var videoGame = await _mediator.Send(new AddVideoGameCommand(newVideoGame));
        return videoGame.Data != 0
            ? Ok(videoGame)
            : BadRequest(videoGame);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame)
    {
        var videoGame = await _mediator.Send(new UpdateVideoGameCommand(id, updatedVideoGame));
        return videoGame.Data != 0
            ? Ok(videoGame)
            : BadRequest(videoGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveVideoGameAsync(int id)
    {
        var videoGame = await _mediator.Send(new RemoveVideoGameCommand(id));
        return videoGame.Data != 0
            ? NoContent()
            : NotFound(videoGame);
    }
}
