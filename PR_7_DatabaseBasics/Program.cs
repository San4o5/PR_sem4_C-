using Npgsql;
using PR_7_DatabaseBasics.Database;
using PR_7_DatabaseBasics.Models;

namespace PR_7_DatabaseBasics;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Підключення
        await using var connection = DbContext.GetConnection();
        await connection.OpenAsync();
        Console.WriteLine("✅ Підключено до PostgreSQL!");

        await CreateTableAsync(connection);
        await InsertPlayerAsync(connection, new Player { Name = "Oleg", Level = 6 });
        await InsertPlayerAsync(connection, new Player { Name = "Enemy", Level = 5 });

        var players = await GetAllPlayersAsync(connection);
        foreach (var p in players)
            Console.WriteLine($"[{p.Id}] {p.Name} — Level {p.Level}");
    }

    private static async Task CreateTableAsync(NpgsqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS players (
                id    SERIAL PRIMARY KEY,
                name  VARCHAR(100) NOT NULL,
                level INT DEFAULT 1
            );
            """;
        // Об'єкт який тримає SQL запит + з'єднання
        await using var cmd = new NpgsqlCommand(sql, connection);
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("📋 Таблиця players готова");
    }

    private static async Task InsertPlayerAsync(NpgsqlConnection connection, Player player)
    {
        // Вставка даних
        const string sql = "INSERT INTO players (name, level) VALUES (@name, @level)";
        await using var cmd = new NpgsqlCommand(sql, connection);
        // Підставляє реальне значення замість @name
        cmd.Parameters.AddWithValue("name", player.Name);
        cmd.Parameters.AddWithValue("level", player.Level);
        await cmd.ExecuteNonQueryAsync();
    }

    private static async Task<List<Player>> GetAllPlayersAsync(NpgsqlConnection connection)
    {
        var players = new List<Player>();
        const string sql = "SELECT * FROM players";
        await using var cmd = new NpgsqlCommand(sql, connection);
        // Читання даних
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            players.Add(new Player
            {
                Id    = reader.GetInt32(0),
                Name  = reader.GetString(1),
                Level = reader.GetInt32(2)
            });
        }

        return players;
    }
}