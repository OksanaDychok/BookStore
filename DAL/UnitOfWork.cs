using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DAL
{
    public class UnitOfWork:IDisposable
    {
        public UnitOfWork(SQLServerContext context)
        {
            this.context = context;
        }
        bool disposed = false;
        SQLServerContext context;
        private SageRepository TR;
        private BookRepository OR;
        private CartItemRepository CIR;
        public SageRepository Sages
        {
            get
            {
                if (TR == null)
                    TR = new SageRepository(context);
                return TR;
            }
        }
        public BookRepository Books
        {
            get
            {
                if (OR == null)
                    OR = new BookRepository(context);
                return OR;
            }
        }

        public CartItemRepository CartItems
        {
            get
            {
                if (CIR == null)
                    CIR = new CartItemRepository(context);
                return CIR;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                    this.disposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UpdateInstructorCourses(int[] selectedSage,Book BookToUpdate, List<Sage> sage_listbefore)
        {
            if (selectedSage == null)
            {
                BookToUpdate.Sages = new List<Sage>();
                return;
            }

            var selectedSagesHS = new HashSet<int>(selectedSage);
            var instructorSages = new HashSet<int>
                (BookToUpdate.Sages.Select(c => c.ID));
            foreach (var sage in context.Sages)
            {
                if (!sage_listbefore.Contains(sage))
                {
                    if (selectedSagesHS.Contains(sage.ID))
                    {
                        BookToUpdate.Sages.Add(sage);
                    }
                }
                else
                {
                    if(!selectedSagesHS.Contains(sage.ID))
                    BookToUpdate.Sages.Remove(sage);
                }
            }
        }
        //private void AddOrUpdateCourses(Sage s, IEnumerable<Book> booklist)
        //{
        //    if (booklist == null) return;

        //    if (s.ID != 0)
        //    {
        //        // Existing user - drop existing courses and add the new ones if any
        //        foreach (var book in s.Books.ToList())
        //        {
        //            s.Books.Remove(book);
        //        }

        //        foreach (var book in booklist.Where(c => c.ID==))
        //        {
        //            s.Books.Add(context.Books.Find(book.ID));
        //        }
        //    }
        //    else
        //    {
        //        // New user
        //        foreach (var assignedCourse in assignedCourses.Where(c => c.Assigned))
        //        {
        //            var course = new Course { CourseID = assignedCourse.CourseID };
        //            db.Courses.Attach(course);
        //            userProfile.Courses.Add(course);
        //        }
        //    }
        //}

    }

}
