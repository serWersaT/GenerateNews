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
    public class VectorsTable : IDisposable, IcruidTable<VectorModel>
    {
        private SQLiteConnection conn;
        public VectorsTable()
        {
            string baseName = Directory.GetCurrentDirectory() + "/VectorsTable.db3";
#if DEBUG
            baseName = @"F:\GIT LAB PROJECT\мое\GenerateNews\v1\GenerateNews\GenerateNewsDAL\DBSQL\VectorsTable.db3";
#endif
           
            if (!File.Exists(baseName))
            {

                SQLiteConnection.CreateFile(baseName);
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = " + baseName))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"CREATE TABLE [VectorsTable] (
                                [Id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            [LevelVector] char(200),
                                            [Vector] char(2000),
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

        public void Insert(VectorModel model)
        {
            bool SelectTest = false;

            using (SQLiteCommand cmd = new SQLiteCommand($@"SELECT * FROM VectorsTable WHERE Vector = '{model.Vector}' and LevelVector = '{model.LevelVector}'"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.StepCount == 0) SelectTest = true;
            }

            if (SelectTest == true)
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = $@"INSERT INTO VectorsTable(LevelVector, Vector, Active)
                                        VALUES ('{model.LevelVector}', '{model.Vector}', '{model.Active}')";
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
            using (SQLiteCommand command = new SQLiteCommand($@"DELETE FROM VectorsTable
                                         WHERE Id = {id}"))
            {
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        public void Update(VectorModel model)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"UPDATE VectorsTable 
                                                                SET 
                                                                LevelVector = '{model.LevelVector}',
                                                                Vector = '{model.Vector}',
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
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM VectorsTable"))
            {
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                count = (reader.Read()) ? reader.GetInt32(0) : 0;
                reader.Close();
            }
            return count;
        }

        public List<VectorModel> SelectWhere(string where)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM VectorsTable b  WHERE a.id >= b.id) AS cnt,
                                                                LevelVector,
                                                                Vector,
                                                                Active
                                                                FROM VectorsTable a   
                                                                where " + where +
                                                                " order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        public List<VectorModel> Select(int? topmin, int? topmax)
        {
            using (SQLiteCommand command = new SQLiteCommand($@"SELECT 
                                                                Id,
                                                                (SELECT count(*) FROM VectorsTable b  WHERE a.id >= b.id) AS cnt,
                                                                LevelVector,
                                                                Vector,
                                                                Active
                                                                FROM VectorsTable a   
                                                                where cnt >= {topmin}
                                                                and cnt <= {topmax}
                                                                order by Id"))
            {
                command.Connection = conn;
                SQLiteDataReader reader = command.ExecuteReader();
                return Reader(reader);
            }
        }

        private List<VectorModel> Reader(SQLiteDataReader reader)
        {
            var ListWord = new List<VectorModel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var wd = new VectorModel();
                    wd.Id = reader.GetInt32(0);
                    wd.LevelVector = reader.GetString(1);
                    wd.Vector = reader.GetString(2);
                    wd.Active = reader.GetInt32(3);
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
