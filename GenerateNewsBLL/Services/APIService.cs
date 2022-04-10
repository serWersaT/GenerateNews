using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateNewsDAL.Interfaces;
using GenerateNewsDAL.Models;
using GenerateNewsDAL.DB;


namespace GenerateNewsBLL.Services
{
    public class APIService : DefaultService
    {
        public List<NewsModel> GetAllNewsDate(DateTime dateFrom, DateTime dateTo)
        {
            var sql = " Created between '" + dateFrom + "' and '" + dateTo + "' ";
            return FuncSelectWhere<NewsModel>(dn, sql);
        }

        public List<NewsModel> GetNewsToday(string theme)
        {
            var sql = " Created > cast(getdate() as date) and Theme = '" + theme + "' ";
            return FuncSelectWhere<NewsModel>(dn, sql);
        }
    }
}
