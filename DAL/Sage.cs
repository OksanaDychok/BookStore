using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace DAL
{
    public class Sage:IEntity
    {
        public Sage() { }
        public int ID { get; set; }
        public string name { get; set; }
        public Nullable<int> age { get; set; }
        public byte[] photo { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
