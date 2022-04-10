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
    public class PhraseService: DefaultService
    {
        public int PhraseCnt()
        {
            return FuncCount<PhraseModel>(dp);
        }

        public bool InsertPhrase(PhraseModel model)
        {
            FuncInsert<PhraseModel>(dp, model);
            return true;
        }

        public bool UpdatePhrase(PhraseModel model)
        {
            FuncUpdate<PhraseModel>(dp, model);
            return true;
        }

        public List<PhraseModel> SelectPhrase(int? topmin, int? topmax)
        {
            return FuncSelect<PhraseModel>(dp, topmin, topmax);
        }
    }
}
