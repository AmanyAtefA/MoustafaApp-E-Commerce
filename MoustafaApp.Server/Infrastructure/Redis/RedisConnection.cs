using StackExchange.Redis;

public class RedisConnection
{
    private readonly Lazy<ConnectionMultiplexer> _connection;

    public RedisConnection(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");

        _connection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(connectionString));
    }

    public IDatabase Database => _connection.Value.GetDatabase();
}