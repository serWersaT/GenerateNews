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
    public class NewsService: DefaultService
    {
        public int NewsCnt()
        {
            return FuncCount<NewsModel>(dn);
        }

        public bool InsertNews(NewsModel model)
        {
            FuncInsert<NewsModel>(dn, model);
            return true;
        }

        public bool UpdateNews(NewsModel model)
        {
            FuncUpdate<NewsModel>(dn, model);
            return true;
        }

        public List<NewsModel> SelectNews(int? topmin, int? topmax)
        {
            return FuncSelect<NewsModel>(dn, topmin, topmax);
        }
    }
}
