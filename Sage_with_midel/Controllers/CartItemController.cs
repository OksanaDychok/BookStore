using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Sage_with_midel.Controllers
{
    public class CartItemController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new SQLServerContext("DefaultConnection"));

        private int IsExisting(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ID == id) return i;
            }
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = IsExisting(id);
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }
        [HttpGet]
        public ActionResult OrderNow()
        {
            return View("Cart");
        }

        public ActionResult OrderNowBook(int id)
        {
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                Book b = new Book();
                b = uow.Books.SelectByID(id);
                CartItem ci = new CartItem();
                ci.BookName = b.name;
                ci.BookPrice = b.price;
                ci.Book_ID = b.ID;
                ci.Quantity = 1;
                cart.Add(ci);
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int k = 0;
                    Book b = new Book();
                    b = uow.Books.SelectByID(id);
                foreach (CartItem citem in cart.ToArray())
                { if (citem.Book_ID == b.ID)
                    {
                        k = 1;
                        citem.Quantity += 1;
                    }
                }
                if(k==0)
                {
                    CartItem ci = new CartItem();
                    ci.BookName = b.name;
                    ci.BookPrice = b.price;
                    ci.Book_ID = b.ID;
                    ci.Quantity = 1;
                    cart.Add(ci);
                }

                
                Session["cart"] = cart;
            }
            return View("Cart");
        }

        [Authorize]
        public ActionResult Buybooks()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            foreach (CartItem c in cart)
            {
                c.DateOrder = DateTime.Now;
                c.UserName = User.Identity.Name;

                uow.CartItems.Create(c);
            }
            uow.Save();
            Session["cart"] = null;
            return RedirectToAction("ViewBook", "Book");
        }
    }
}