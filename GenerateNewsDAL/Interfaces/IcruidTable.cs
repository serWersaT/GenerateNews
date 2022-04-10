using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsDAL.Interfaces
{
    public interface IcruidTable<M>
    {
        void Dispose();
        int Count();
        void Update(M model);
        void Insert(M model);
        List<M> Select(int? topmin, int? topmax);
        List<M> SelectWhere(string where);
    }
}
