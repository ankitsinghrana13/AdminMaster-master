using AdminMaster.Repository.Interface;
using AdminMaster.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Controllers
{
    public class BooksController : Controller
    {
        private GenericInterface<BookWithAuthorViewModel> BookService;

            public BooksController(GenericInterface<BookWithAuthorViewModel> _Book)
        {
            BookService = _Book;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetBooks()
        {
            var books = BookService.GetData();
            return Json(books);
        }
    }
}
