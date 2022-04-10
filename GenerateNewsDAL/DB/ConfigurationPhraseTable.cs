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
    public class ConfigurationPhraseTable : IDisposable, IcruidTable<ConfigPhraseModel>
    {
        private SQLiteConnection conn;
        public ConfigurationPhraseTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/ConfigurationPhraseTable.db3";
#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\ConfigurationPhraseTable.db3";
#endif
            if (!File.Exists(baseName))
            {
                SQLiteConnection.CreateFile(baseName);
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [ConfigurationPhraseTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [PhraseContains] char(200),
                                            [Word] char(200),
                                            [Theme] char(200),
                                            [Form] char(200),
                                            [EmotionLevel] integer,
                                            [stringTheme] char(200),
                                            [MixTheme] char(200),
                                            [NotMixTheme] char(200),
                                            [Grouped] char(200),
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


        public void Insert(ConfigPhraseModel model)
        {
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM ConfigurationPhraseTable WHERE PhraseContains = '{model.PhraseContains}'
                                                                                                and Word = '{model.Word}'
                                                                                                and Theme = '{model.Theme}'
                                                                                                and Form = '{model.Form}'
                                                                                                and EmotionLevel = {model.EmotionLevel}
                                                                                                and stringTheme = '{model.StringTheme}'
                                                                                                and MixTheme = '{model.MixTheme}'
                                                                                                and NotMixTheme = '{model.NotMixTheme}'
                                                                                                and Grouped = '{model.Group}'

"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO ConfigurationPhraseTable(PhraseContains, Word, Theme, Form, EmotionLevel, stringTheme, MixTheme, 
                                                        NotMixTheme, Grouped, Active)
                                        VALUES ('{model.PhraseContains}', '{model.Word}', '{model.Theme}', '{model.Form}', '{model.EmotionLevel}', '{model.StringTheme}',
                                                '{model.MixTheme}', '{model.NotMixTheme}', '{model.Group}', '{model.Active}')";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM ConfigurationPhraseTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(ConfigPhraseModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE ConfigurationPhraseTable 
                                                                SET 
                                                                PhraseContains = '{model.PhraseContains}',
                                                                Word = '{model.Word}',
                                                                Theme = '{model.Theme}',
                                                                Form = '{model.Form}',
                                                                EmotionLevel = '{model.EmotionLevel}',
                                                                stringTheme = {model.StringTheme},
                                                                MixTheme = '{model.MixTheme}',
                                                                NotMixTheme = '{model.NotMixTheme}',
                                                                Grouped = '{model.Group}',
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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM ConfigurationPhraseTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<ConfigPhraseModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ConfigurationPhraseTable b  WHERE a.id >= b.id) AS cnt,
                                                                PhraseContains,
                                                                Word,
                                                                Theme,
                                                                Form,
                                                                EmotionLevel,
                                                                stringTheme,
                                                                MixTheme,
                                                                NotMixTheme,
                                                                Grouped,
                                                                Active
                                                                FROM ConfigurationPhraseTable a   
                                                                where " + where + 
                                                                "order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<ConfigPhraseModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ConfigurationPhraseTable b  WHERE a.id >= b.id) AS cnt,
                                                                PhraseContains,
                                                                Word,
                                                                Theme,
                                                                Form,
                                                                EmotionLevel,
                                                                stringTheme,
                                                                MixTheme,
                                                                NotMixTheme,
                                                                Grouped,
                                                                Active
                                                                FROM ConfigurationPhraseTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<ConfigPhraseModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<ConfigPhraseModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new ConfigPhraseModel();
                    wd.Id = reader.GetInt32(0);
                    wd.RowNum = reader.GetInt32(1);
                    wd.PhraseContains = reader.GetString(2);
                    wd.Word = reader.GetString(3);
                    wd.Theme = reader.GetString(4);
                    wd.Form = reader.GetString(5);
                    wd.EmotionLevel = reader.GetInt32(6);
                    wd.StringTheme = reader.GetString(7);
                    wd.MixTheme = reader.GetString(8);
                    wd.NotMixTheme = reader.GetString(9);
                    wd.Group = reader.GetString(10);
                    wd.Active = reader.GetInt32(11);
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
