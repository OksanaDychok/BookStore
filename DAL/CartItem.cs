using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace DAL
{
    public class CartItem : IEntity
    {
        public int Book_ID { get; set; }
        public System.DateTime DateOrder { get; set;}
        public string UserName { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }
        public String BookName { get; set; }
        public float BookPrice { get; set; }
    }
}
