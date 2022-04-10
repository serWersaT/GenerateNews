using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateNewsDAL.Models;
using GenerateNewsDAL.Interfaces;
using GenerateNewsDAL.DB;

namespace GenerateNewsBLL.Services
{
    public class ConfigWordService: DefaultService
    {
        public int ConfigWordCnt()
        {
            return FuncCount<ConfigWordModel>(cw);
        }

        public bool InsertConfigWord(ConfigWordModel model)
        {
            FuncInsert<ConfigWordModel>(cw, model);
            return true;
        }

        public bool UpdateConfigWord(ConfigWordModel model)
        {
            FuncUpdate<ConfigWordModel>(cw, model);
            return true;
        }

        public List<ConfigWordModel> SelectConfigWord(int? topmin, int? topmax)
        {
            return FuncSelect<ConfigWordModel>(cw, topmin, topmax);
        }

        public List<ConfigWordModel> FindConfigWord(ConfigWordModel model, int topmax)
        {
            var sql = " a.Active = 1 ";
            if (!string.IsNullOrEmpty(model.EmotionLevel.ToString())) sql += " and a.EmotionLevel = " + model.EmotionLevel.ToString() + " ";
            if (!string.IsNullOrEmpty(model.FormWord)) sql += " and a.FormWord like '%" + model.FormWord.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.GenderForm)) sql += " and a.GenderForm like '%" + model.GenderForm.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.Mix)) sql += " and a.Mix like '%" + model.Mix.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.MixFormWord)) sql += " and a.MixFormWord like '%" + model.MixFormWord.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.MixGenderForm)) sql += " and a.MixGenderForm like '%" + model.MixGenderForm.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.MixTypeWord)) sql += " and a.MixTypeWord like '%" + model.MixTypeWord.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.NotMixFormWord)) sql += " and a.NotMixFormWord like '%" + model.NotMixFormWord.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.NotMixGenderForm)) sql += " and a.NotMixGenderForm like'%" + model.NotMixGenderForm.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.NotMixTypeWord)) sql += " and a.NotMixTypeWord like '%" + model.NotMixTypeWord.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.Not_Mix)) sql += " and a.Not_Mix like '%" + model.Not_Mix.ToString() + "%' ";
            if (model.RowNum != 0) sql += " and RowNum = " + model.RowNum.ToString() + " ";
            if (!string.IsNullOrEmpty(model.str)) sql += " and a.str like '%" + model.str.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.strContains)) sql += " and a.strContains like '%" + model.strContains.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.strMixContains)) sql += " and a.strMixContains like '%" + model.strMixContains.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.strNotMixContains)) sql += " and a.strNotMixContains like '%" + model.strNotMixContains.ToString() + "%' ";
            if (!string.IsNullOrEmpty(model.TypeWord)) sql += " and a.TypeWord like '%" + model.TypeWord.ToString() + "%' ";
            //sql += " ORDER BY Id OFFSET 0 ROWS FETCH NEXT " + topmax.ToString() + " ROWS ONLY";
            return FuncSelectWhere<ConfigWordModel>(cw, sql);
        }
    }
}
