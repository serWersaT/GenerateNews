using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using GenerateNewsDAL.Interfaces;
using GenerateNewsDAL.Models;
using System.IO;

namespace GenerateNewsDAL.DB
{
    class ThemesTable : IDisposable, IcruidTable<ThemesModel>
    {
        private SQLiteConnection conn;
        public ThemesTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/ThemesTable.db3";
            if (!File.Exists("ThemesTable.db3"))
            {
#if DEBUG
                baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\DB\ThemesTable.db3";
#endif

                SQLiteConnection.CreateFile(baseName);
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [ThemesTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [Themes] char(200),
                                            [Active] bit
                                );";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            conn = new SQLiteConnection($"Data Source = " + baseName);
            conn.Open();
        }

        public void Insert(ThemesModel model)
        {
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM ThemesTable WHERE Themes = '{model.Themes}'"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO ThemesTable(Themes, Active)
                                        VALUES ('{model.Themes}', '{model.Active}')";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Update(model);
            }
        }

        public void Delete(int id)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM ThemesTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(ThemesModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE ThemesTable 
                                                                SET 
                                                                Themes = '{model.Themes}',
                                                                Active = '{model.Active}'
                                                            WHERE Id = {model.Id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public int Count()
        {
            int count = 0;
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM ThemesTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<ThemesModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ThemesTable b  WHERE a.id >= b.id) AS cnt,
                                                                Themes,
                                                                Active
                                                                FROM ThemesTable a   
                                                                where " + where + 
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<ThemesModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ThemesTable b  WHERE a.id >= b.id) AS cnt,
                                                                Themes,
                                                                Active
                                                                FROM ThemesTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<ThemesModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<ThemesModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new ThemesModel();
                    wd.Id = reader.GetInt32(0);
                    wd.Themes = reader.GetString(1);
                    wd.Active = reader.GetBoolean(3);
                    ListWord.Add(wd);
                }
                reader.Close();
            }
            return ListWord;
        }


        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }

    }
}
