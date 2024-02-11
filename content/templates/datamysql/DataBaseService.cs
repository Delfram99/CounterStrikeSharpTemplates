using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Logging;

namespace CSSharpTemplates
{
    public class DataBaseService
    {
        private readonly ILogger<DataBaseService> _logger;
        private readonly string _connectionString;
        private readonly CSSharpTemplatesConfig _config;

        public DataBaseService(CSSharpTemplatesConfig config)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<DataBaseService>();

            _config = config;
            _connectionString = BuildDatabaseConnectionString();
        }

        private string BuildDatabaseConnectionString()
        {
            if (string.IsNullOrWhiteSpace(_config.DatabaseHost) || 
                string.IsNullOrWhiteSpace(_config.DatabaseUser) || 
                string.IsNullOrWhiteSpace(_config.DatabasePassword) || 
                string.IsNullOrWhiteSpace(_config.DatabaseName) ||
                _config.DatabasePort == 0)
            {
                throw new InvalidOperationException("Database is not set in the configuration file");
            }

            MySqlConnectionStringBuilder builder = new()
            {
                Server = _config.DatabaseHost,
                Port = (uint)_config.DatabasePort,
                UserID = _config.DatabaseUser,
                Password = _config.DatabasePassword,
                Database = _config.DatabaseName,
                Pooling = true,
            };

            return builder.ConnectionString;
        }

        private async Task<MySqlConnection> GetOpenConnectionAsync()
        {
            try
            {
                var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while opening database connection");
                throw;
            }
        }

        /* 
            This method iterates through a list of predefined tables and checks if each table exists in the database.
            If a table doesn't exist, it creates the table using the corresponding SQL query.
            It is recommended not to use files for SQL queries as it can pose security risks.
        */
        public async Task TestAndCheckDataBaseTableAsync() 
        {
            try
            {
                await using var connection = await GetOpenConnectionAsync();

                _logger.LogInformation("Database connection successful!");

                await using var transaction = await connection.BeginTransactionAsync();

                try
                {
                    string[] tables = { "commands", "player_commands" };
                    foreach (var table in tables)
                    {
                        var tableExists = await connection.QueryFirstOrDefaultAsync<string>(
                            $"SHOW TABLES LIKE @table;", new { table }, transaction: transaction) != null;

                        if (!tableExists)
                        {
                            string createTableQuery = table switch
                            {
                                "commands" => @"
                                    CREATE TABLE `commands` (
                                        `id` INT AUTO_INCREMENT PRIMARY KEY, 
                                        `command` VARCHAR(255) NOT NULL UNIQUE
                                    )",
                                "player_commands" => @"
                                    CREATE TABLE `player_commands` (
                                        `id` INT AUTO_INCREMENT PRIMARY KEY, 
                                        `player_id` BIGINT NOT NULL, 
                                        `player_name` VARCHAR(255) NOT NULL, 
                                        `command` VARCHAR(255) NOT NULL, 
                                        `count` INT NOT NULL, 
                                        UNIQUE KEY `player_command` (`player_id`, `command`)
                                    )",
                                _ => throw new InvalidOperationException($"Unknown table: {table}")
                            };

                        await connection.ExecuteAsync(createTableQuery, transaction: transaction);
                    }
                }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error while checking database table");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database connection failed");
                throw;
            }
        }

        public async Task ExecuteMultipleSqlWithTransactionAsync(IEnumerable<(string sql, object param)> queries)
        {
            await using var connection = await GetOpenConnectionAsync();
            await using var transaction = await connection.BeginTransactionAsync();
            try
            {
                foreach (var query in queries)
                {
                    await connection.ExecuteAsync(query.sql, query.param, transaction: transaction);
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error while executing multiple SQL commands within a transaction");
                throw;
            }
        }
        public async Task<T?> ExecuteSqlAsync<T>(string sql, object param)
        {
            try
            {
                await using var connection = await GetOpenConnectionAsync();

                var result = await connection.QueryAsync<T>(sql, param);

                if (!result.Any())
                {
                    _logger.LogWarning("SQL command returned no results");
                    return default;
                }

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing SQL command");
                throw;
            }
        }
    }
}