using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string News { get; set; }
        public DateTime? Created { get; set; }
        public string Theme { get; set; }
        public int EmotionLevel { get; set; }
        public int Active { get; set; }
    }
}
