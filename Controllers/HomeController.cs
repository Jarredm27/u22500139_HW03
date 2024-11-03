using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HW03.Models;
using System.Data.Entity;
using System.Net.NetworkInformation;
using System.Net;
using System.Web.UI;
using System.Web.Services.Description;

namespace HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        private const int PageSize = 10;

        public async Task<ActionResult> Index(int? page)
        {
            var viewModel = new CombinedViewModel
            {
                Authors = await db.authors.ToListAsync(),
                Types = await db.types.ToListAsync()

            };

            int pageNumber = page ?? 1;
            int pageSize = 10; // Set your desired page size here.

            // Paginate the students data.
            var studentsQuery = db.students.OrderBy(s => s.studentId).AsQueryable();
            var studentsPagedList = studentsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Students = studentsPagedList;

            // Paginate the books data.
            var booksQuery = db.books
                .Include(b => b.author)
                .Include(b => b.type)
                .OrderBy(b => b.bookId)
                .AsQueryable();
            var booksPagedList = booksQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Books = booksPagedList;

            return View(viewModel);
        }

        public async Task<ActionResult> Maintain(int? page)
        {

            var viewModel = new MaintainVM
            {
                students = await db.students.ToListAsync(),
                books = await db.books.ToListAsync()
            };

            int pageNumber = page ?? 1;
            int pageSize = 10; // Set your desired page size here.

            // Paginate the authors data.
            var authorsQuery = db.authors.OrderBy(s => s.authorId).AsQueryable();
            var authorPagedList = authorsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.authors = authorPagedList;

            // Paginate the types data.
            var typesQuery = db.types.OrderBy(s => s.typeId).AsQueryable();
            var typesPagedList = typesQuery.ToPagedList(pageNumber, pageSize);
            viewModel.types = typesPagedList;

            // Paginate the types data.
            var borrowsQuery = db.borrows
               .Include(b => b.book)
               .OrderBy(b => b.borrowId)
               .AsQueryable();
            var borrowsPagedList = borrowsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.borrows = borrowsPagedList;

            return View(viewModel);
        }

        public ActionResult Report()
        {
            return View();
        }

        // Authors
        public async Task<ActionResult> authorsIndex()
        {
            return View(await db.authors.ToListAsync());
        }

        // GET: authors/Details/5
        public async Task<ActionResult> authorsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = await db.authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: authors/Create
        public ActionResult authorsCreate()
        {
            return View();
        }

        // POST: authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> authorsCreate([Bind(Include = "authorId,name,surname")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("authorsIndex");
            }

            return View(author);
        }

        // GET: authors/Edit/5
        public async Task<ActionResult> authorsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = await db.authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> authorsEdit([Bind(Include = "authorId,name,surname")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("authorsIndex");
            }
            return View(author);
        }

        // GET: authors/Delete/5
        public async Task<ActionResult> authorsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = await db.authors.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> authorsDeleteConfirmed(int id)
        {
            var author = await db.authors.FindAsync(id);

            // Check if the author exists
            if (author == null)
            {
                return HttpNotFound(); // Author not found
            }

            // Remove the author
            db.authors.Remove(author);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                return new HttpStatusCodeResult(HttpStatusCode.OK); // Indicate success
            }
            catch (Exception ex)
            {
                // Log the error (not shown)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error deleting author: " + ex.Message);
            }
        }

        // Books
        public async Task<ActionResult> booksIndex()
        {
            var books = db.books.Include(b => b.author).Include(b => b.type);
            return View(await books.ToListAsync());
        }

        //Borrows
        public async Task<ActionResult> borrowsIndex()
        {
            var borrows = db.borrows.Include(b => b.book).Include(b => b.student);
            return View(await borrows.ToListAsync());
        }

        //Students
        public async Task<ActionResult> studentsIndex()
        {
            return View(await db.students.ToListAsync());
        }

        //Types
        public async Task<ActionResult> typesIndex()
        {
            return View(await db.types.ToListAsync());
        }
    }
}