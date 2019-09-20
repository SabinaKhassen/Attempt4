using Attempt4.ViewModels;
using AutoMapper;
using BussinessLayer.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Attempt4.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        protected IMapper mapper;

        public BookController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var bookList = bookBO.GetBooksList();
            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            ViewBag.Books = bookList.Select(m => mapper.Map<BookViewModel>(m)).ToList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
            return View();
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<BookViewModel>(bookBO);

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookViewModel>(bookBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(m => mapper.Map<AuthorViewModel>(m)).ToList(), "Id", "LastName");

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BookViewModel model)
        {
            var bookBO = mapper.Map<BookBO>(model);
            //if (ModelState.IsValid)
            //{
            bookBO.Save();
            return RedirectToActionPermanent("Index", "Book");
            //}
            //else return View(model);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);

            return RedirectToActionPermanent("Index", "Book");
        }
    }
}
