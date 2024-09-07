using System.Data;
using Dapper;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Infrastructure.Mapper;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Infrastructure.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly IDbConnection _dbConnection;

    public VideoGameRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync()
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<ICollection<VideoGameResponse>>();

        try
        {
            string sql = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                            FROM video_games;";

            _dbConnection.Open();

            var videoGames = await _dbConnection.QueryAsync<VideoGame>(sql);
            var videoGamesResult = videoGames.Select(x => mapper.VideoGameToVideoGameResult(x)).ToList();

            serviceResponse.Data = videoGamesResult;
        }
        catch (Exception ex)
        {
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
            string sql = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                             FROM video_games
                             WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = await _dbConnection.QueryFirstOrDefaultAsync<VideoGame>(sql, new { Id = id });
            var videoGameResult = videoGame
                ?? throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = mapper.VideoGameToVideoGameResult(videoGameResult);
        }
        catch (Exception ex)
        {
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
            string sql = @"INSERT INTO video_games(title, publisher, developer, release_date)
                                   VALUES(@Title, @Publisher, @Developer, @ReleaseDate);";

            _dbConnection.Open();

            var videoGame = mapper.VideoGameInputToVideoGame(newVideoGame);
            var videoGameResult = await _dbConnection.ExecuteAsync(sql, videoGame);

            if (videoGameResult == 0)
                throw new Exception("An error ocurred when inserting a new register.");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
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

            if (videoGameResult == 0)
                throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
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
            string sql = @"DELETE
                          FROM video_games
                          WHERE id = @Id;";

            _dbConnection.Open();

            var videoGameResult = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            if (videoGameResult == 0)
                throw new Exception($"Video Game with id {id} not found!");

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
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
