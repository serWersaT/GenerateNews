using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateNewsDAL.Models;
using GenerateNewsDAL.Interfaces;
using GenerateNewsDAL.DB;
using GenerateNewsBLL.Const;

namespace GenerateNewsBLL.Services
{
    public class DefaultService
    {
        public WordsTable dw = new WordsTable();
        public PhrasesTable dp = new PhrasesTable();
        public NewsTable dn = new NewsTable();
        public VectorsTable vt = new VectorsTable();
        public ConfigurationWordTable cw = new ConfigurationWordTable();
        public ConfigurationPhraseTable cp = new ConfigurationPhraseTable();
        public DefaultConst consts = new DefaultConst();

        //WordsTable dw = new WordsTable();
        public int FuncCount<M>(IcruidTable<M> Icrud)
        {
            return Icrud.Count();
        }

        public void FuncInsert<M>(IcruidTable<M> Icrud, M model)
        {
            Icrud.Insert(model);
        }

        public void FuncUpdate<M>(IcruidTable<M> Icrud, M model)
        {
            Icrud.Update(model);
        }

        public List<M> FuncSelect<M>(IcruidTable<M> Icrud, int? topmin, int? topmax)
        {
           return Icrud.Select(topmin, topmax);
        }

        public List<M> FuncSelectWhere<M>(IcruidTable<M> Icrud, string where)
        {
            return Icrud.SelectWhere(where);
        }
    }
}
