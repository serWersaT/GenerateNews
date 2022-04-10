using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class ConfigWordModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string strContains { get; set; }     //начинается/содержит/заканчивается
        public string str { get; set; }     // что именно содержит
        public string TypeWord { get; set; }    //тип слова (глагол, наречие и т.д.)
        public string GenderForm { get; set; }      //род слова
        public string FormWord { get; set; }        //форма слова (ед.ч.  мн.ч.)
        public int? EmotionLevel { get; set; }       //эмоциональный уровень
        public string strMixContains { get; set; }      //начинается/содержит/заканчивается соседнее слово - разрешено
        public string Mix { get; set; }             //строка
        public string strNotMixContains { get; set; }       //начинается/содержит/заканчивается соседнее слово  - запрещено
        public string Not_Mix { get; set; }         //строка
        public string NotMixTypeWord { get; set; }      //запрещено размещать по соседству с типом слова
        public string NotMixGenderForm { get; set; }        //запрещено размещать по соседству с родом слова
        public string NotMixFormWord { get; set; }      //запрещено размещать по соседству с формой слова
        public string MixTypeWord { get; set; }         //разрешено размещать по соседству с типом слова
        public string MixGenderForm { get; set; }   //разрешено размещать по соседству с родом слова
        public string MixFormWord { get; set; }     //разрешено размещать по соседству с формой слова
        public string Grouped { get; set; }   //для группировки условий
        public int Active { get; set; }        //откл/вкл
    }
}
