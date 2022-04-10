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
    public class WordService: DefaultService
    {
        public int WordCnt()
        {
            return FuncCount<WordModel>(dw);
        }

        public bool InsertWord(WordModel model)
        {
            FuncInsert<WordModel>(dw, model);
            return true;
        }

        public bool UpdateWord(WordModel model)
        {
            FuncUpdate<WordModel>(dw, model);
            return true;
        }

        public List<WordModel> Select(int? topmin, int? topmax)
        {
            return FuncSelect<WordModel>(dw, topmin, topmax);
        }
    }
}
