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

            return View();
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            var bookBO = DependencyResolver.Current.GetService<BookBO>();
            var authors = DependencyResolver.Current.GetService<AuthorBO>();
            var genres = DependencyResolver.Current.GetService<GenreBO>();
            var model = mapper.Map<BookViewModel>(bookBO);

            if (id != null)
            {
                var bookBOList = bookBO.GetBooksListById(id);
                model = mapper.Map<BookViewModel>(bookBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            ViewBag.Authors = new SelectList(authors.GetAuthorsList().Select(m => mapper.Map<AuthorViewModel>(m)).ToList(), "Id", "LastName");
            //ViewBag.Genres = new SelectList(genres.GetGenresList().Select(m => mapper.Map<GenreViewModel>(m)).ToList(), "Id", "Name");

            return View(model);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BookViewModel model, HttpPostedFileBase upload)
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

            bookBO.Save();

            return RedirectToActionPermanent("Index", "Book");

            //// массив для хранения бинарных данных файла
            //byte[] imageData;
            //using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
            //{
            //    imageData = new byte[fs.Length];
            //    fs.Read(imageData, 0, imageData.Length);
            //}

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

        [HttpGet]
        public ActionResult GenreDropDown()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>().GetGenresList();
            var genreList = genreBO.Select(m => mapper.Map<GenreViewModel>(m)).ToList();
            return Json(genreList.Select(g => new { g.Id, g.Name }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
