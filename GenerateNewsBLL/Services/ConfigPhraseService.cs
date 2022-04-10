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
    public class ConfigPhraseService: DefaultService
    {
        public int ConfigPhraseCnt()
        {
            return FuncCount<ConfigPhraseModel>(cp);
        }

        public bool InsertConfigPhrase(ConfigPhraseModel model)
        {
            FuncInsert<ConfigPhraseModel>(cp, model);
            return true;
        }

        public bool UpdateConfigPhrase(ConfigPhraseModel model)
        {
            FuncUpdate<ConfigPhraseModel>(cp, model);
            return true;
        }

        public List<ConfigPhraseModel> SelectConfigPhrase(int? topmin, int? topmax)
        {
            return FuncSelect<ConfigPhraseModel>(cp, topmin, topmax);
        }
    }
}
