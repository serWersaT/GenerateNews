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
    public class NewsTable : IDisposable, IcruidTable<NewsModel>
    {
        private SQLiteConnection conn;
        public NewsTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/NewsTable.db3";

#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\NewsTable.db3";
#endif

            if (!File.Exists(baseName))
            {

                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [NewsTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [Created] datetime,
                                            [News] char(4000),
                                            [Theme] char(200),
                                            [EmotionLevel] integer,
                                            [Active] integer
                                );";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            conn = new SQLiteConnection($"Data Source = " + baseName);
            conn.Open();
        }


        public void Insert(NewsModel model)
        {
            model.News = model.News.ToLower();
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM NewsTable WHERE News = '{model.News}'"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO NewsTable(Created, News, Theme, EmotionLevel, Active)
                                        VALUES ('{DateTime.Now}', '{model.News}', '{model.Theme}', '{model.EmotionLevel}', '{model.Active}')";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM NewsTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(NewsModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE NewsTable 
                                                                SET 
                                                                Created = '{model.Created}',
                                                                News = '{model.News}',
                                                                Theme = '{model.Theme}', 
                                                                EmotionLevel = '{model.EmotionLevel}',
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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM NewsTable ORDER BY News"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }


        public List<NewsModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM NewsTable b  WHERE a.id >= b.id) AS cnt,
                                                                Created,
                                                                News,
                                                                Theme,
                                                                EmotionLevel,
                                                                Active
                                                                FROM NewsTable a   
                                                                where " + where + 
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<NewsModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM NewsTable b  WHERE a.id >= b.id) AS cnt,
                                                                Created,
                                                                News,
                                                                Theme,
                                                                EmotionLevel,
                                                                Active
                                                                FROM NewsTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<NewsModel> Reader(SQLiteDataReader reader)
        {
            var ListNews = new List<NewsModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var qwe = reader.GetString(2);
                    var wd = new NewsModel();
                    wd.Id = reader.GetInt32(0);
                    wd.RowNum = reader.GetInt32(1);
                    //wd.Created = Convert.ToDateTime(reader.GetString(2));
                    wd.Created = wd.Created;
                    wd.News = reader.GetString(3);
                    wd.Theme = reader.GetString(4);
                    wd.EmotionLevel = reader.GetInt32(5);
                    wd.Active = reader.GetInt32(6);
                    ListNews.Add(wd);
                }
                reader.Close();
            }
            return ListNews;
        }


        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
