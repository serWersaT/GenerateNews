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
    public class WordsTable : IDisposable, IcruidTable<WordModel>
    {
        private SQLiteConnection conn;
        public WordsTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/WordsTable.db3";

#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\WordsTable.db3";
#endif

            if (!File.Exists(baseName))
            {

                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [WordsTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [Word] char(100) NOT NULL,
                                            [Type] char(100),
                                            [Gender_Type] char(100),
                                            [FormWord] char(100),
                                            [EmotionLevel] integer,
                                            [Synonim] char(2000),
                                            [Mix] char(2000),
                                            [Not_Mix] char(2000),
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

        public void Insert(WordModel model)
        {
            model.Word = model.Word.ToLower();
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM WordsTable WHERE Word = '{model.Word}'"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO WordsTable(Word, Type, Gender_Type, FormWord, EmotionLevel, Synonim, Mix, Not_Mix, Active)
                                        VALUES ('{model.Word}', '{model.Type}', '{model.Gender_Type}', '{model.FormWord}', '{model.EmotionLevel}', '{model.Synonim}', '{model.Mix}', '{model.Not_Mix}', 1)";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM WordsTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(WordModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE WordsTable 
                                                                SET 
                                                                Word = '{model.Word}',
                                                                Type = '{model.Type}',
                                                                Gender_Type = '{model.Gender_Type}', 
                                                                FormWord = '{model.FormWord}',
                                                                Synonim = '{model.Synonim}', 
                                                                Mix = '{model.Mix}', 
                                                                Not_Mix = '{model.Not_Mix}',
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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM WordsTable ORDER BY Word"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<WordModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM WordsTable b  WHERE a.id >= b.id) AS cnt,
                                                                Word,
                                                                Type,
                                                                Gender_Type,
                                                                FormWord,
                                                                Synonim,
                                                                Mix,
                                                                Not_Mix,
                                                                EmotionLevel,
                                                                Active
                                                                FROM WordsTable a   
                                                                where " + where  + 
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<WordModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM WordsTable b  WHERE a.id >= b.id) AS cnt,
                                                                Word,
                                                                Type,
                                                                Gender_Type,
                                                                FormWord,
                                                                Synonim,
                                                                Mix,
                                                                Not_Mix,
                                                                EmotionLevel,
                                                                Active
                                                                FROM WordsTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<WordModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<WordModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new WordModel();
                    wd.Id = reader.GetInt32(0);
                    wd.RowNum = reader.GetInt32(1);
                    wd.Word = reader.GetString(2);
                    wd.Type = reader.GetString(3);
                    wd.Gender_Type = reader.GetString(4);
                    wd.FormWord = reader.GetString(5);
                    wd.Synonim = reader.GetString(6);
                    wd.Mix = reader.GetString(7);
                    wd.Not_Mix = reader.GetString(8);
                    wd.EmotionLevel = (string.IsNullOrEmpty(reader.GetInt32(9).ToString())) ? 0 : reader.GetInt32(9);
                    wd.Active = reader.GetInt32(10);
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
