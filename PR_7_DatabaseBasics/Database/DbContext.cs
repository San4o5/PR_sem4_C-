using Npgsql;

namespace PR_7_DatabaseBasics.Database;

public static class DbContext
{
    // Рядок підключення — адреса де знаходиться БД
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=postgres;Username=admin;Password=admin";

    // Метод який створює об'єкт з'єднання.
    public static NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}