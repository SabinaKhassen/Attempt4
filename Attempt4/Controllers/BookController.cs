using Attempt4.ViewModels;
using AutoMapper;
using BussinessLayer.BussinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
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
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();

            ViewBag.Books = bookList.Select(m => mapper.Map<BookViewModel>(m)).ToList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreViewModel>(m)).ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial/BookPartialView", ViewBag.Books);
            }

            return View();
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            //var authors = DependencyResolver.Current.GetService<AuthorBO>();
            //var genres = DependencyResolver.Current.GetService<GenreBO>();
            var model = mapper.Map<BookViewModel>(bookBO);

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookViewModel>(bookBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            //ViewBag.AuthorsSelectList = new SelectList(authors.GetAuthorsList().Select(m => mapper.Map<AuthorViewModel>(m)).ToList(), "Id", "LastName");
            //ViewBag.Genres = new SelectList(genres.GetGenresList().Select(m => mapper.Map<GenreViewModel>(m)).ToList(), "Id", "Name");

            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial/EditPartialView", model);
            }

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BookViewModel model, HttpPostedFileBase upload, int genre, int author)
        {
            string str = "test";
            var bookBO = mapper.Map<BookBO>(model);

            if (ModelState.IsValid && upload != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    imageData = binaryReader.ReadBytes(upload.ContentLength);
                }
                // установка массива байтов
                bookBO.ImageData = imageData;
            }
            else
            {
                bookBO.ImageData = new byte[str.Length];
            }
            bookBO.GenreId = genre;
            bookBO.AuthorId = author;
            bookBO.Save();

            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();

            var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreViewModel>(m)).ToList();

            return PartialView("Partial/BookPartialView", books.Select(m => mapper.Map<BookViewModel>(m)).ToList());
        }

        // GET: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var book = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(id);
            book.Delete(id);
            var bookBO = DependencyResolver.Current.GetService<BookBO>().GetBooksList();
            if (Request.IsAjaxRequest())
            {
                var authorList = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
                var genreList = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();
                ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
                ViewBag.Genres = genreList.Select(m => mapper.Map<GenreViewModel>(m)).ToList();

                return PartialView("Partial/BookPartialView", bookBO.Select(m => mapper.Map<AuthorViewModel>(m)).ToList());
            }

            return RedirectToActionPermanent("Index", "Book");
        }

        [HttpGet]
        public ActionResult GenreDropDown()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();
            var genreList = genreBO.Select(m => mapper.Map<GenreViewModel>(m)).ToList();
            return Json(genreList.Select(g => new { g.Id, g.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AuthorDropDown()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();
            var authorList = authorBO.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
            return Json(authorList.Select(g => new { g.Id, g.LastName }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
