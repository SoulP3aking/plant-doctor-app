using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace PlantDoctorApp
{
    public class HistoryItem
    {
        public string ImagePath { get; set; } = "";
        public string Name { get; set; } = "";
        public string Condition { get; set; } = "";
        public string Advice { get; set; } = "";
    }

    public class HistoryService
    {
        private readonly string _dbPath = "history.db";

        public HistoryService()
        {
            using var db = new SqliteConnection($"Data Source={_dbPath}");
            db.Open();
            var cmd = db.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT, path TEXT, name TEXT, condition TEXT, advice TEXT);";
            cmd.ExecuteNonQuery();
        }

        public void SaveResult(string path, string name, string condition, string advice)
        {
            using var db = new SqliteConnection($"Data Source={_dbPath}");
            db.Open();
            var cmd = db.CreateCommand();
            cmd.CommandText = "INSERT INTO history (path, name, condition, advice) VALUES ($p, $n, $c, $a);";
            cmd.Parameters.AddWithValue("$p", path);
            cmd.Parameters.AddWithValue("$n", name);
            cmd.Parameters.AddWithValue("$c", condition);
            cmd.Parameters.AddWithValue("$a", advice);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<HistoryItem> LoadHistory()
        {
            using var db = new SqliteConnection($"Data Source={_dbPath}");
            db.Open();
            var cmd = db.CreateCommand();
            cmd.CommandText = "SELECT path, name, condition, advice FROM history ORDER BY id DESC";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new HistoryItem
                {
                    ImagePath = reader.GetString(0),
                    Name = reader.GetString(1),
                    Condition = reader.GetString(2),
                    Advice = reader.GetString(3)
                };
            }
        }
    }
}
