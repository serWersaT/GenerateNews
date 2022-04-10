using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class AnalizWordModel
    {
        public string Word { get; set; }
        public int weight { get; set; }
        public string Type { get; set; }
    }
}
