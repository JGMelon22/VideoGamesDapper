using System.Data;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Infrastructure.Repositories;

public class VideoGameRepisitory : IVideoGameRepisitory
{
    private readonly IDbConnection _dbConnection;

    public VideoGameRepisitory(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ServiceResponse<VideoGameResponse>> AddVideoGameAsync(VideoGameInput newVideoGame)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<VideoGameResponse>> GetVideoGameByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<bool>> RemoveVideoGameAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<VideoGameResponse>> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame)
    {
        throw new NotImplementedException();
    }
}
