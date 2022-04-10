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
    public class ConfigurationWordTable : IDisposable, IcruidTable<ConfigWordModel>
    {
        private SQLiteConnection conn;
        public ConfigurationWordTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/ConfigurationWordTable.db3";

#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\ConfigurationWordTable.db3";
#endif

            if (!File.Exists(baseName))
            {
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [ConfigurationWordTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [strContains] char(200),
                                            [str] char(200),
                                            [TypeWord] char(200),
                                            [GenderForm] char(200),
                                            [FormWord] char(200),
                                            [EmotionLevel] integer,
                                            [strMixContains] char(200),
                                            [Mix] char(500),
                                            [strNotMixContains] char(200),
                                            [Not_Mix] char(200),
                                            [NotMixTypeWord] char(200),
                                            [NotMixGenderForm] char(200),
                                            [NotMixFormWord] char(200),
                                            [MixTypeWord] char(200),
                                            [MixGenderForm] char(200),
                                            [MixFormWord] char(200),
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

        public void Insert(ConfigWordModel model)
        {
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM ConfigurationWordTable WHERE strContains = '{model.strContains}'
                                                                                                and str = '{model.str}'
                                                                                                and TypeWord = '{model.TypeWord}'
                                                                                                and GenderForm = '{model.GenderForm}'
                                                                                                and FormWord = '{model.FormWord}'
                                                                                                and EmotionLevel = {model.EmotionLevel}
                                                                                                and strMixContains = '{model.strMixContains}'
                                                                                                and Mix = '{model.Mix}'
                                                                                                and strNotMixContains = '{model.strNotMixContains}'
                                                                                                and Not_Mix = '{model.Not_Mix}'
                                                                                                and NotMixTypeWord = '{model.NotMixTypeWord}'
                                                                                                and NotMixGenderForm = '{model.NotMixGenderForm}'
                                                                                                and NotMixFormWord = '{model.NotMixFormWord}'
                                                                                                and MixTypeWord = '{model.MixTypeWord}'
                                                                                                and MixGenderForm = '{model.MixGenderForm}'
                                                                                                and MixFormWord = '{model.MixFormWord}'
                                                                                                and Grouped = '{model.Grouped}'

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
                    command.CommandText = $@"INSERT INTO ConfigurationWordTable(strContains, str, TypeWord, GenderForm, FormWord, EmotionLevel, strMixContains, 
                                                        Mix, strNotMixContains, Not_Mix, NotMixTypeWord, NotMixGenderForm, NotMixFormWord, MixTypeWord, MixGenderForm, Grouped,
                                                        MixFormWord, Active)
                                        VALUES ('{model.strContains}', '{model.str}', '{model.TypeWord}', '{model.GenderForm}', '{model.FormWord}', {model.EmotionLevel},
                                                '{model.strMixContains}', '{model.Mix}', '{model.strNotMixContains}', '{model.Not_Mix}', '{model.NotMixTypeWord}',
                                                '{model.NotMixGenderForm}', '{model.NotMixFormWord}', '{model.MixTypeWord}', '{model.MixGenderForm}',
                                                '{model.MixFormWord}', '{model.Grouped}', {model.Active})";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM ConfigurationWordTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(ConfigWordModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE ConfigurationWordTable 
                                                                SET 
                                                                strContains = '{model.strContains}',
                                                                str = '{model.str}',
                                                                TypeWord = '{model.TypeWord}',
                                                                GenderForm = '{model.GenderForm}',
                                                                FormWord = '{model.FormWord}',
                                                                EmotionLevel = {model.EmotionLevel},
                                                                strMixContains = '{model.strMixContains}',
                                                                Mix = '{model.Mix}',
                                                                strNotMixContains = '{model.strNotMixContains}',
                                                                Not_Mix = '{model.Not_Mix}',
                                                                NotMixTypeWord = '{model.NotMixTypeWord}',
                                                                NotMixGenderForm = '{model.NotMixGenderForm}',
                                                                NotMixFormWord = '{model.NotMixFormWord}',
                                                                MixTypeWord = '{model.MixTypeWord}',
                                                                MixGenderForm = '{model.MixGenderForm}',
                                                                MixFormWord = '{model.MixFormWord}',
                                                                Grouped = '{model.Grouped}',
                                                                Active = {model.Active}
                                                            WHERE Id = {model.Id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public int Count()
        {
            int count = 0;
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM ConfigurationWordTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<ConfigWordModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ConfigurationWordTable b  WHERE a.id >= b.id) AS cnt,
                                                                strContains,
                                                                str,
                                                                TypeWord,
                                                                GenderForm,
                                                                FormWord,
                                                                EmotionLevel,
                                                                strMixContains,
                                                                Mix,
                                                                strNotMixContains,
                                                                Not_Mix,
                                                                NotMixTypeWord,
                                                                NotMixGenderForm,
                                                                NotMixFormWord,
                                                                MixTypeWord,
                                                                MixGenderForm,
                                                                MixFormWord,
                                                                Grouped,
                                                                Active
                                                                FROM ConfigurationWordTable a   
                                                                where " + where ))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<ConfigWordModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM ConfigurationWordTable b  WHERE a.id >= b.id) AS cnt,
                                                                strContains,
                                                                str,
                                                                TypeWord,
                                                                GenderForm,
                                                                FormWord,
                                                                EmotionLevel,
                                                                strMixContains,
                                                                Mix,
                                                                strNotMixContains,
                                                                Not_Mix,
                                                                NotMixTypeWord,
                                                                NotMixGenderForm,
                                                                NotMixFormWord,
                                                                MixTypeWord,
                                                                MixGenderForm,
                                                                MixFormWord,
                                                                Grouped,
                                                                Active
                                                                FROM ConfigurationWordTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<ConfigWordModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<ConfigWordModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new ConfigWordModel();
                    wd.Id = reader.GetInt32(0);
                    wd.RowNum = reader.GetInt32(1);
                    wd.strContains = reader.GetString(2);
                    wd.str = reader.GetString(3);
                    wd.TypeWord = reader.GetString(4);
                    wd.GenderForm = reader.GetString(5);
                    wd.FormWord = reader.GetString(6);
                    wd.EmotionLevel = reader.GetInt32(7);
                    wd.strMixContains = reader.GetString(8);
                    wd.Mix = reader.GetString(9);
                    wd.strNotMixContains = reader.GetString(10);
                    wd.Not_Mix = reader.GetString(11);
                    wd.NotMixTypeWord = reader.GetString(12);
                    wd.NotMixGenderForm = reader.GetString(13);
                    wd.NotMixFormWord = reader.GetString(14);
                    wd.MixTypeWord = reader.GetString(15);
                    wd.MixGenderForm = reader.GetString(16);
                    wd.MixFormWord = reader.GetString(17);
                    wd.Grouped = reader.GetString(18);
                    wd.Active = reader.GetInt32(19);
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
