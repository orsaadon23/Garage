using MySql.Data.MySqlClient;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<string>> GetVehicleLicensePlates()
    {
        var licensePlates = new List<string>();

        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT LicensePlate FROM vehicles";
        using var command = new MySqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            licensePlates.Add(reader.GetString(0));
        }

        return licensePlates;
    }
}
