using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class ConfigPhraseModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string PhraseContains { get; set; } //начинается/содержит/заканчивается
        public string Word { get; set; }        //само слово
        public string Theme { get; set; }     //выбранное слово есть/не главное слово (подлежащее)
        public string Form { get; set; }    //форма предложения вопросительная, утвердительная 
        public int EmotionLevel { get; set; }       //эмоциональный уровень
        public string StringTheme { get; set; }     //тема предложения
        public string MixTheme { get; set; }        //разрешить распологать с предложениями данной темы
        public string NotMixTheme { get; set; }     //запретить распологать с предложениями данной темы
        public string Group { get; set; }   //для группировки условий
        public int Active { get; set; }

    }
}
