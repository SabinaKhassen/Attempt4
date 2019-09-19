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
    public class AuthorController : Controller
    {
        // GET: Author
        protected IMapper mapper;

        #region before adding GenericRepository
        //private AuthorRepository authorRepository;

        //public AuthorController()
        //{
        //    authorRepository = new AuthorRepository(new Model1());
        //}
        //public ActionResult Index()
        //{
        //    var model = authorRepository.GetAll();
        //    return View(model);
        //}
        #endregion

        #region Generic Repository
        //private IRepository<Authors> authorRepository;

        //public AuthorController()
        //{
        //    authorRepository = new Repository<Authors>();
        //}
        //public ActionResult Index()
        //{
        //    var model = authorRepository.GetAll();
        //    return View(model);
        //}
        #endregion

        public AuthorController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var authorList = authorBO.GetAuthorsList();
            ViewBag.Authors = authorList.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();
            return View();
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int? id)
        {
            var authorBO = DependencyResolver.Current.GetService<AuthorBO>();
            var model = mapper.Map<AuthorViewModel>(authorBO);
            if (id != null)
            {
                var authorBOList = authorBO.GetAuthorsListById(id);
                model = mapper.Map<AuthorViewModel>(authorBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";
            return View(model);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(AuthorViewModel model)
        {
            var authorBO = mapper.Map<AuthorBO>(model);
            //if (ModelState.IsValid)
            //{
                authorBO.Save();
            return RedirectToActionPermanent("Index", "Author");
            //}
            //else return View(model);
        }

        // POST: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var author = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsListById(id);
            author.Delete(id);

            return RedirectToActionPermanent("Index", "Author");
        }
    }
}
