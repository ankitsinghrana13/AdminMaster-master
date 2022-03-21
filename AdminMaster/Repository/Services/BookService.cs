using AdminMaster.Models;
using AdminMaster.Repository.Interface;
using AdminMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Repository.Services
{
    public class BookService:IBook,GenericInterface<BookWithAuthorViewModel>
    {
        private MSDBContext dbContext;
        public BookService()
        {
            dbContext = new MSDBContext();
        }

        public List<BookWithAuthorViewModel> GetData()
        {
            var Books = (from Book in dbContext.Books
                         join
                         author in dbContext.Author
                         on Book.Authar_Id equals author.Id
                         select new BookWithAuthorViewModel()
                         {
                             id=Book.id,
                             Title=Book.Title,
                             Price=Book.Price,
                             Quantity=Book.Quantity,
                             Published_On=Book.Published_On,
                             AuthorName=author.Name,
                             AuthorEmail=author.Email,
                             AuthorMobile=author.Mobile
                         }
                       ).ToList();
            return Books;
        }
    }
}
