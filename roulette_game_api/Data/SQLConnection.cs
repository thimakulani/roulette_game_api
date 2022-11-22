using Microsoft.Data.Sqlite;

namespace roulette_game_api.Data
{
    public class SQLConnection
    {
        public SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source=DB/roulett.sqlite");
        }
    }
}
