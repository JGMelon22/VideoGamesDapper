using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Interfaces;

public interface IVideoGameRepository
{
    Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync();
    Task<ServiceResponse<VideoGameResponse>> GetVideoGameByIdAsync(int id);
    Task<ServiceResponse<VideoGameResponse>> AddVideoGameAsync(VideoGameInput newVideoGame);
    Task<ServiceResponse<VideoGameResponse>> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame);
    Task<ServiceResponse<bool>> RemoveVideoGameAsync(int id);
}
