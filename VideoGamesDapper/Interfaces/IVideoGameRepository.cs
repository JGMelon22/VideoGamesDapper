using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Interfaces;

public interface IVideoGameRepository
{
    Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync();
    Task<ServiceResponse<VideoGameResponse>> GetVideoGameByIdAsync(int id);
    Task<ServiceResponse<int>> AddVideoGameAsync(VideoGameInput newVideoGame);
    Task<ServiceResponse<int>> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame);
    Task<ServiceResponse<int>> RemoveVideoGameAsync(int id);
}
