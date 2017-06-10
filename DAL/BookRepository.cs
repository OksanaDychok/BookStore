using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Repository;

namespace DAL
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(SQLServerContext context):base(context)
        {
            
        }
    }
}
