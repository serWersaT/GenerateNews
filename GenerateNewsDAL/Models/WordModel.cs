using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class WordModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string Word { get; set; }
        public string Type { get; set; }
        public string Gender_Type { get; set; }
        public string FormWord { get; set; }
        public int? EmotionLevel { get; set; }
        public string Synonim { get; set; }
        public string Mix { get; set; }
        public string Not_Mix { get; set; }
        public int Active { get; set; }
    }
}
