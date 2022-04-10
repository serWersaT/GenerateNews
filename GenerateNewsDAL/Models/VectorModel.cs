using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class VectorModel
    {
        public int Id { get; set; }
        public string LevelVector { get; set; }     //предложение или текст
        public string Vector { get; set; }     //вектор
        public int Active { get; set; }
    }
}
