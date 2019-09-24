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
    public class GenreController : Controller
    {
        // GET: Genre
        protected IMapper mapper;

        public GenreController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var genreList = genreBO.GetGenresList();
            ViewBag.Genres = genreList.Select(m => mapper.Map<GenreViewModel>(m)).ToList();

            return View();
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            var genreBO = DependencyResolver.Current.GetService<GenreBO>();
            var model = mapper.Map<GenreViewModel>(genreBO);
            if (id != null)
            {
                var genreList = genreBO.GetGenresListById(id);
                model = mapper.Map<GenreViewModel>(genreList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            return View(model);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(GenreViewModel model)
        {
            var genreBO = mapper.Map<GenreBO>(model);
            genreBO.Save();

            return RedirectToActionPermanent("Index", "Genre");
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {
            var genre = DependencyResolver.Current.GetService<GenreBO>().GetGenresListById(id);
            genre.Delete(id);

            return RedirectToActionPermanent("Index", "Genre");
        }
    }
}
