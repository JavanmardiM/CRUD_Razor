using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Razor.Pages.BookList
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        [TempData]
        public string Massage { get; set; }
        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var bookFromDb = _db.Books.Find(id);
                bookFromDb.Author = Book.Author;
                bookFromDb.ISBN = Book.ISBN;
                bookFromDb.Name = Book.Name;

                await _db.SaveChangesAsync();
                Massage = "Book successfully updated.";

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}