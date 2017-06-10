using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Repository;

namespace DAL
{
    public class SageRepository : BaseRepository<Sage>
    {
        public SageRepository(SQLServerContext context):base(context)
        {
            
        }
    }
}
