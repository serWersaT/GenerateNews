using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class PhraseModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string Phrase { get; set; }
        public string Form { get; set; }
        public string Theme { get; set; }
        public int EmotionLevel { get; set; }
        public string Mix { get; set; }
        public string Not_Mix { get; set; }
        public int Active { get; set; }
    }
}
