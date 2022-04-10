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
    public class PhrasesTable : IDisposable, IcruidTable<PhraseModel>
    {
        private SQLiteConnection conn;
        public PhrasesTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/PhrasesTable.db3";

#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\PhrasesTable.db3";
#endif  

            if (!File.Exists(baseName))
            {
                //SQLiteConnection.CreateFile(baseName);
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [PhrasesTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [Phrase] char(2000) NOT NULL,
                                            [Form] integer,
                                            [Theme] char(200),
                                            [EmotionLevel] integer,
                                            [Mix] char(4000),
                                            [Not_Mix] char(4000),
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

        public void Insert(PhraseModel model)
        {
            model.Phrase = model.Phrase.ToLower();
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM PhrasesTable WHERE Phrase = '{model.Phrase}'"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO PhrasesTable(Phrase, Form, Theme, EmotionLevel, Mix, Not_Mix, Active)
                                        VALUES ('{model.Phrase}', '{model.Form}', '{model.Theme}', '{model.EmotionLevel}', '{model.Mix}', '{model.Not_Mix}', 1)";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM PhrasesTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(PhraseModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE PhrasesTable 
                                                                SET 
                                                                Phrase = '{model.Phrase}',
                                                                Form = '{model.Form}',
                                                                Theme = '{model.Theme}', 
                                                                EmotionLevel = '{model.EmotionLevel}',
                                                                Mix = '{model.Mix}', 
                                                                Not_Mix = '{model.Not_Mix}',
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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM PhrasesTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<PhraseModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM PhrasesTable b  WHERE a.id >= b.id) AS cnt,
                                                                Phrase,
                                                                Form,
                                                                Theme,
                                                                EmotionLevel,
                                                                Mix,
                                                                Not_Mix,
                                                                Active
                                                                FROM PhrasesTable a   
                                                                where " + where +
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<PhraseModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM PhrasesTable b  WHERE a.id >= b.id) AS cnt,
                                                                Phrase,
                                                                Form,
                                                                Theme,
                                                                EmotionLevel,
                                                                Mix,
                                                                Not_Mix,
                                                                Active
                                                                FROM PhrasesTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<PhraseModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<PhraseModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new PhraseModel();
                    wd.Id = reader.GetInt32(0);
                    wd.RowNum = reader.GetInt32(1);
                    wd.Phrase = reader.GetString(2);
                    wd.Form = reader.GetString(3);
                    wd.Theme = reader.GetString(4);
                    wd.EmotionLevel = reader.GetInt32(5);
                    wd.Mix = reader.GetString(6);
                    wd.Not_Mix = reader.GetString(7);
                    wd.Active = reader.GetInt32(8);
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
