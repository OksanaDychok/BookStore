using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Sage_with_midel.Controllers
{
    public class BookController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new SQLServerContext("DefaultConnection"));

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.sagelist = new MultiSelectList(uow.Sages.GetAll(), "ID", "name");
            Book b = new Book();
            return View(b);
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Create")]
        public ActionResult Create(Book b)
        {
            ViewBag.sagelist = new MultiSelectList(uow.Sages.GetAll(), "ID", "name", b.SelectedBooks);
                foreach (var sageID in b.SelectedBooks)
                {
                    Sage sage = uow.Sages.SelectByID(sageID);
                    b.Sages.Add(sage);
                }
            uow.Books.Create(b);
            uow.Save();
            return RedirectToAction("ViewBook");
        }
        public ActionResult ViewBook()
        {
            return View(uow.Books.GetAll());
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book b = uow.Books.SelectByID(id);
            if (b == null)
                return HttpNotFound();
            Book bk = new Book();
            ViewBag.sagelist = new MultiSelectList(uow.Sages.GetAll(), "ID", "name");

            Book book = new Book();
            book.countpage = b.countpage;
            book.description = b.description;
            book.name = b.name;
            book.price = b.price;

            return View(book);
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Book b)
        {
            Book book = uow.Books.SelectByID(b.ID);
            List<Sage> before_s = new List<Sage>();
            foreach (Sage s in book.Sages)
                before_s.Add(s);
            ViewBag.sagelist = new MultiSelectList(uow.Sages.GetAll(), "ID", "name", b.SelectedBooks);
            uow.UpdateInstructorCourses(b.SelectedBooks, b, before_s);
            uow.Books.Update(b);

            uow.Save();
            return RedirectToAction("ViewBook");
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = uow.Books.SelectByID(id);
            return View(b);
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.Books.Delete(id);
            return RedirectToAction("ViewBook");
        }

    }
}