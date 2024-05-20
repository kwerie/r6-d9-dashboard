using Microsoft.EntityFrameworkCore;

namespace backend.Configuration;

public class Config
{
    public required string ConnectionString { get; init; }
    public readonly MySqlServerVersion MysqlServerVersion = new(new Version(8, 0, 29));
}