using Microsoft.Extensions.Logging;

namespace CSSharpTemplates
{
    public class PlayerCommandInfo
    {
        public int Count { get; set; }
        public string? CommandName { get; set; }
    }

    public partial class CSSharpTemplates
    {
        private const string InsertCommandSql = @"
            INSERT INTO commands (command) 
            VALUES (@command) 
            ON DUPLICATE KEY UPDATE command = VALUES(command)";
        private const string InsertPlayerCommandSql = @"
            INSERT INTO player_commands (player_id, player_name, command, count) 
            VALUES (@playerId, @playerName, @command, 1) 
            ON DUPLICATE KEY UPDATE count = count + 1";
        private const string UpdatePlayerCommandSqlTransaction = @"
            UPDATE player_commands 
            SET count = 0 
            WHERE player_id = @playerId AND command = @command AND count > 5";

        private const string SelectCountSql = @"
            SELECT count 
            FROM player_commands 
            WHERE player_id = @playerId AND command = @command";
        private const string SelectCommandSql = @"
            SELECT command 
            FROM player_commands 
            WHERE player_id = @playerId AND command = @command";
        private const string SelectCountAndCommandSql = @"
            SELECT count, command as CommandName 
            FROM player_commands 
            WHERE player_id = @playerId AND command = @command";

        private async Task CheckPlayerCommand(string command, string playerName, ulong playerId) 
        {
            var queries = new List<(string sql, object param)>
            {
                (InsertCommandSql, new { command }),
                (InsertPlayerCommandSql, new { playerId, playerName, command }),
                (UpdatePlayerCommandSqlTransaction, new { playerId, command }),
            };

            /*
                If you want to add a query to the transaction based on some condition,
                you can add it to the queries list.
                
                if(someCondition)
                {
                    queries.Add((UpdatePlayerCommandSql, new { playerId, command }));
                }
            */
            
            if(_dataBaseService == null)
            {
                Logger.LogError($"[{nameof(CSSharpTemplates)}] Database service is not initialized");
                return;
            }
            
            try
            {
                // Example of how to execute multiple SQL commands with a transaction using a list of queries
                await _dataBaseService.ExecuteMultipleSqlWithTransactionAsync(queries);
                
                // Example of retrieving int and string data from the database without a transaction
                int commandCount = await _dataBaseService.ExecuteSqlAsync<int>(SelectCountSql, new { playerId, command });
                string commandName = await _dataBaseService.ExecuteSqlAsync<string>(SelectCommandSql, new { playerId, command }) ?? string.Empty;

                // Example of how to insert data into the database without a transaction. The same method can be used for update or delete operations
                await _dataBaseService.ExecuteSqlAsync<int>(InsertPlayerCommandSql, new { playerId, playerName, command });

                // Example of retrieving an object from the database without using a transaction
                var result = await _dataBaseService.ExecuteSqlAsync<PlayerCommandInfo>(SelectCountAndCommandSql, new { playerId, command });

                Logger.LogInformation("[{0}] Command({1}) count: {2}", nameof(CSSharpTemplates), commandName, commandCount);

                if (result != null)
                {
                    Logger.LogInformation("[{0}] Command({1}) count: {2}", nameof(CSSharpTemplates), result.CommandName ?? string.Empty, result.Count);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[{nameof(CSSharpTemplates)}] Error while checking player commands");
                throw;
            }
        }
    }
}