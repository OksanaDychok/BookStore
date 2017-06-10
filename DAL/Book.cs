using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Web.Mvc;

namespace DAL
{
    public class Book:IEntity
    {
        public Book()
        {
            Sages = new List<Sage>();
        }
        public int ID { get; set; }
        public string name { get; set; }
        public Nullable<int> countpage { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int[] SelectedBooks { get; set; }
        public virtual ICollection<Sage> Sages { get; set; }
    }
}
