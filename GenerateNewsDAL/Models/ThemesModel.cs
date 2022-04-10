using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Models
{
    public class ThemesModel
    {
        public int Id { get; set; }
        public string Themes { get; set; }     //доступная тема
        public bool Active { get; set; }
    }
}
