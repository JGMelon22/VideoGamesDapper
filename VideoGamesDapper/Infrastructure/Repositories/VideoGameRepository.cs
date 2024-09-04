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

    public async Task<ServiceResponse<VideoGameResponse>> AddVideoGameAsync(VideoGameInput newVideoGame)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<VideoGameResponse>();

        try
        {
            string insertQuery = @"INSERT INTO video_games(title, publisher, developer, release_date)
                                   VALUES(@Title, @Publisher, @Developer, @ReleaseDate);";

            string selectQuery = @"SELECT id AS Id,
                                          title AS Title,
                                          publisher AS Publisher,
                                          developer AS Developer,
                                          release_date AS ReleaseDate
                                   FROM video_games
                                   WHERE id = @Id;";

            _dbConnection.Open();

            var videoGameId = await _dbConnection.ExecuteAsync(insertQuery, newVideoGame);
            var videoGame = await _dbConnection.QueryFirstOrDefaultAsync<VideoGame>(selectQuery, new { Id = videoGameId });

            var videoGameResult = mapper.VideoGameToVideoGameResult(videoGame!);

            _dbConnection.Close();

            serviceResponse.Data = videoGameResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<ICollection<VideoGameResponse>>> GetAllVideoGamesAsync()
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<ICollection<VideoGameResponse>>();

        try
        {
            string query = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                            FROM video_games;";

            _dbConnection.Open();

            var videoGames = await _dbConnection.QueryAsync<VideoGame>(query);
            var videoGamesResult = videoGames.Select(x => mapper.VideoGameToVideoGameResult(x)).ToList();

            _dbConnection.Close();

            serviceResponse.Data = videoGamesResult;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<VideoGameResponse>> GetVideoGameByIdAsync(int id)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<VideoGameResponse>();

        try
        {
            string query = @"SELECT id AS Id,
                                    title AS Title,
                                    publisher AS Publisher,
                                    developer AS Developer,
                                    release_date AS ReleaseDate
                             FROM video_games
                             WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = await _dbConnection.QueryFirstOrDefaultAsync<VideoGame>(query, new { Id = id });
            var videoGameResult = videoGame
                ?? throw new Exception($"Video Game with id {id} not found!");

            _dbConnection.Close();

            serviceResponse.Data = mapper.VideoGameToVideoGameResult(videoGameResult);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveVideoGameAsync(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            string query = @"DELETE FROM video_games
                             WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = await _dbConnection.ExecuteAsync(query, new { Id = id });

            _dbConnection.Close();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<VideoGameResponse>> UpdateVideoGameAsync(int id, VideoGameInput updatedVideoGame)
    {
        VideoGameMapper mapper = new VideoGameMapper();
        var serviceResponse = new ServiceResponse<VideoGameResponse>();

        try
        {
            string updateQuery = @"UPDATE video_games
                                   SET title = @Title,
                                       publisher = @Publisher,
                                       developer = @Developer,
                                       release_date = @ReleaseDate
                                  WHERE id = @Id;";

            string selectQuery = @"SELECT id AS Id,
                                          title AS Title,
                                          publisher AS Publisher,
                                          developer AS Developer,
                                          release_date AS ReleaseDate
                                   FROM video_games
                                   WHERE id = @Id;";

            _dbConnection.Open();

            var videoGame = await _dbConnection.QueryFirstOrDefaultAsync<VideoGame>(selectQuery, new { Id = id });
            var videoGameResult = videoGame
                ?? throw new Exception($"Video Game with id {id} not found!");

            mapper.ApplyUpdate(updatedVideoGame, videoGame);

            await _dbConnection.ExecuteAsync(updateQuery, new
            {
                videoGame.Title,
                videoGame.Publisher,
                videoGame.Developer,
                videoGame.ReleaseDate,
                Id = id
            });

            _dbConnection.Close();

            serviceResponse.Data = mapper.VideoGameToVideoGameResult(videoGameResult);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
