using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sage_with_midel.Controllers
{
    public class SageController : Controller
    {
        private UnitOfWork uow = new UnitOfWork(new SQLServerContext("DefaultConnection"));

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Sage());
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Create")]
        public ActionResult Create(Sage sage, HttpPostedFileBase file)
        {
            if (file != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    sage.photo = ms.GetBuffer();
                }
            }
            uow.Sages.Create(sage);
            uow.Save();
            return RedirectToAction("ViewSage");
        }
        public ActionResult ViewSage()
        {
            return View(uow.Sages.GetAll());
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sage s = uow.Sages.SelectByID(id);
            if (s == null)
                return HttpNotFound();
            return View(s);
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Sage s, HttpPostedFileBase file)
        {
            if (file != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    s.photo = ms.GetBuffer();
                }
            }
            uow.Sages.Update(s);
            uow.Save();
            return RedirectToAction("ViewSage");
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Sage s = uow.Sages.SelectByID(id);
            return View(s);
        }

        [Authorize(Users = "oxana@gmail.com")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.Sages.Delete(id);
            return RedirectToAction("ViewSage");
        }
    }
}