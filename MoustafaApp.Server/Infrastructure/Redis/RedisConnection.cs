using StackExchange.Redis;

public class RedisConnection
{
    private readonly ConnectionMultiplexer _connection;

    public RedisConnection(string connectionString)
    {
        _connection = ConnectionMultiplexer.Connect(connectionString);
    }

    public IDatabase Database => _connection.GetDatabase();
}