using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateNewsDAL.Models;
using GenerateNewsDAL.DB;

namespace GenerateNewsBLL.Services
{
    public class DownLevelService: DefaultService
    {
        public string GetPhraseTheme()
        {
            var sql = " Type = 'главное существительное' ";
            var listword = FuncSelectWhere(dw, sql).ToArray();
            var cnt = listword.Count();
            if (cnt < 1)
            {
                sql = " Type = 'существительное' ";
                listword = FuncSelectWhere(dw, sql).ToArray();
                cnt = listword.Count();
            }
            Random rnd = new Random();
            int val = (cnt == 0) ? rnd.Next(0, cnt) : 0;
            return listword[val].Word;
        }

        public int GetLengthPhrase()
        {
            Random rnd = new Random();
            return rnd.Next(0, 10);
        }

        public List<string> GetVector(string level)
        {
            var sql = " LevelVector = '" + level + "' and Active = 1 ";
            return FuncSelectWhere(vt, sql).Select(x => x.Vector).ToList();
        }

        public string GetConfigWordSql(List<ConfigWordModel> model, string flagMix)
        {
            string sql = "";
            foreach (var z in model)
            {
                if (z.Active == 1 && z.strContains == "начинается")
                {
                    sql += GetSql(z, flagMix);
                }
                if (z.Active == 1 && z.strContains == "содержит")
                {
                    sql += GetSql(z, flagMix);
                }
                if (z.Active == 1 && z.strContains == "равно")
                {
                    sql += GetSql(z, flagMix);
                }
                if (z.Active == 1 && z.strContains == "заканчивается")
                {
                    sql += GetSql(z, flagMix);
                }
            }
            return sql;
        }

        public string GetSql(ConfigWordModel model, string flagMix)
        {
            var result = "";
            var notmixwords = model.Not_Mix.Split(' ');
            string sqlnotmix = "";
            foreach (var c in notmixwords) { sqlnotmix += "'" + c + "',"; }

            result += " and Word not in (" + sqlnotmix.TrimEnd(',') + ") ";

            if (model.strNotMixContains == "начинается") { result += " and Word not like '" + model.Not_Mix + "%' "; }
            if (model.strNotMixContains == "содержит") { result += " and Word not like '%" + model.Not_Mix + "%' "; }
            if (model.strNotMixContains == "равно") { result += " and Word = '" + model.Not_Mix + "' "; }
            if (model.strNotMixContains == "заканчивается") { result += " and Word not like '%" + model.Not_Mix + "' "; }

            if (flagMix == "on")
            {
                string sqlmix = "";
                var mixwords = model.Mix.Split(' ');
                foreach (var c in mixwords) { sqlmix += "'" + c + "',"; }
                result += " and Word in (" + sqlmix.TrimEnd(',') + ") ";
                if (model.strMixContains == "начинается") { result += " and Word like '" + model.Mix + "%' "; }
                if (model.strMixContains == "содержит") { result += " and Word like '%" + model.Mix + "%' "; }
                if (model.strMixContains == "равно") { result += " and Word = '" + model.Mix + "' "; }
                if (model.strMixContains == "заканчивается") { result += " and Word like '%" + model.Mix + "' "; }
            }
            return result;
        }

        public List<AnalizWordModel> GetAnalizeWords(string[] mass)
        {
            var AnalizWord = new List<AnalizWordModel>();
            foreach (var word in mass)
            {
                AnalizWordModel model = new AnalizWordModel();
                model.Word = word;
                var sql = " Word = '" + model.Word + "' ";
                if (string.IsNullOrEmpty(AnalizWord.FirstOrDefault(x => x.Word == word).Word))
                {
                    //model.weight = 1;
                    model.Type = FuncSelectWhere<WordModel>(dw, sql).FirstOrDefault().Type;
                    AnalizWord.Add(model);
                }
                else
                {
                    var _analizWord = AnalizWord.FirstOrDefault(x => x.Word == word);
                    //_analizWord.weight += 1;
                    _analizWord.Type = FuncSelectWhere<WordModel>(dw, sql).FirstOrDefault().Type;
                }
            }

            return AnalizWord;
        }
        public string GenerateScheme()
        {
            var result = "";
            var cnt = GetRandomCount(5, 20);
            for (int i = 0; i < cnt; i++)
            {
                var r = GetRandomCount(1, 10);
                result += r.ToString() + "-";
            }
            return result.TrimEnd('-');
        }

        public int GetRandomCount(int mincount, int maxcount)
        {
            Random rmd = new Random();
            return rmd.Next(mincount, maxcount);
        }

        public List<string> GetVector()
        {
            Random rmd = new Random();
            var conversation_length = rmd.Next(5, 20);
            var vector = new List<string>();
            for (int i = 0; i < conversation_length; i++)
            {
                var gettypeword = rmd.Next(1, 7);
                vector.Add(consts.GetTypeWordString(gettypeword));
            }
            return vector;
        }

        public string GetGeneralWord()
        {
            var sql = " Active = 1 and Type = 'главное существительное' ";
            var listword = FuncSelectWhere<WordModel>(dw, sql);
            if (listword.Count() < 1)
            {
                sql = " Active = 1 and Type = 'существительное' ";
                listword = FuncSelectWhere<WordModel>(dw, sql);
            }
            var rmd = GetRandomCount(0, listword.Count());
            return listword[rmd].Word;
        }
    }
}
