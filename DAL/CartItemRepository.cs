using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace DAL
{
    public class CartItemRepository : BaseRepository<CartItem>
    {
        public CartItemRepository(SQLServerContext context) : base(context)
        {

        }
    }
}
