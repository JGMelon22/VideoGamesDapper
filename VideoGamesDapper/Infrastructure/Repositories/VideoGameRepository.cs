using System.Data;
using System.Reflection;
using Dapper;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Infrastructure.Mapper;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Infrastructure.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ILogger<VideoGameRepository> _logger;

    public VideoGameRepository(IDbConnection dbConnection, ILogger<VideoGameRepository> logger)
    {
        _dbConnection = dbConnection;
        _logger = logger;
    }

    public async Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync()
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<ICollection<VideoGameResponse>>();

        try
        {
            var methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            string sql = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                            FROM video_games;";

            _dbConnection.Open();

            var videoGames = await _dbConnection.QueryAsync<VideoGame>(sql);

            _logger.LogInformation("{@MethodName} - {videoGames}: {@VideoGames}", methodNameLog, nameof(videoGames), videoGames);

            var videoGamesResult = videoGames.Select(x => mapper.VideoGameToVideoGameResult(x)).ToList();

            serviceResponse.Data = videoGamesResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{ClassName} -> {MethodName}] Error: {ErrorMessage}", GetType().Name, MethodBase.GetCurrentMethod()?.Name, ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        finally
        {
            _dbConnection.Close();
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<VideoGameResponse>> GetVideoGameByIdAsync(int id)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<VideoGameResponse>();

        try
        {
            var methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            string sql = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                             FROM video_games
                             WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = await _dbConnection.QueryFirstOrDefaultAsync<VideoGame>(sql, new { Id = id });

            _logger.LogInformation("{@MethodName} - {videoGame}: {@VideoGame}", methodNameLog, nameof(videoGame), videoGame);

            var videoGameResult = videoGame
                ?? throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = mapper.VideoGameToVideoGameResult(videoGameResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{ClassName} -> {MethodName}] Error: {ErrorMessage}", GetType().Name, MethodBase.GetCurrentMethod()?.Name, ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        finally
        {
            _dbConnection.Close();
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> AddVideoGameAsync(VideoGameInput newVideoGame)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<int>();

        try
        {
            var methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            string sql = @"INSERT INTO video_games(title, publisher, developer, release_date)
                                  VALUES(@Title, @Publisher, @Developer, @ReleaseDate);";

            _dbConnection.Open();

            var videoGame = mapper.VideoGameInputToVideoGame(newVideoGame);

            _logger.LogInformation("{@MethodName} - {videoGame}: {@VideoGame}", methodNameLog, nameof(videoGame), videoGame);

            var videoGameResult = await _dbConnection.ExecuteAsync(sql, videoGame);

            if (videoGameResult == 0)
                throw new Exception("An error ocurred while inserting a new register.");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{ClassName} -> {MethodName}] Error: {ErrorMessage}", GetType().Name, MethodBase.GetCurrentMethod()?.Name, ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        finally
        {
            _dbConnection.Close();
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<int>();

        try
        {
            var methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            string sql = @"UPDATE video_games
                             SET title = @Title,
                                 publisher = @Publisher,
                                 developer = @Developer,
                                 release_date = @ReleaseDate
                             WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = mapper.VideoGameInputToVideoGame(updatedVideoGame);
            videoGame.Id = id;

            var videoGameResult = await _dbConnection.ExecuteAsync(sql, videoGame);

            _logger.LogInformation("{@MethodName} - {updatedVideoGame}: {@UpdatedVideoGame}", methodNameLog, nameof(updatedVideoGame), updatedVideoGame);

            if (videoGameResult == 0)
                throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{ClassName} -> {MethodName}] Error: {ErrorMessage}", GetType().Name, MethodBase.GetCurrentMethod()?.Name, ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        finally
        {
            _dbConnection.Close();
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<int>> RemoveVideoGameAsync(int id)
    {
        var serviceResponse = new ServiceResponse<int>();

        try
        {
            var methodNameLog = $"[{GetType().Name} -> {MethodBase.GetCurrentMethod()!.ReflectedType!.Name}]";

            string sql = @"DELETE
                          FROM video_games
                          WHERE id = @Id;";

            _dbConnection.Open();

            var videoGameResult = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            _logger.LogInformation("{@MethodName} - {videoGameResult}: {@VideoGameResult}", methodNameLog, nameof(videoGameResult), videoGameResult);

            if (videoGameResult == 0)
                throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{ClassName} -> {MethodName}] Error: {ErrorMessage}", GetType().Name, MethodBase.GetCurrentMethod()?.Name, ex.Message);

            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        finally
        {
            _dbConnection.Close();
        }

        return serviceResponse;
    }
}
