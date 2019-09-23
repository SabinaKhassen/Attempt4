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
    public class UserController : Controller
    {
        // GET: User
        protected IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var userList = userBO.GetUsersList();
            ViewBag.Users = userList.Select(m => mapper.Map<UserViewModel>(m)).ToList();

            return View();
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var model = mapper.Map<UserViewModel>(userBO);
            if (id != null)
            {
                var userList = userBO.GetUsersListById(id);
                model = mapper.Map<UserViewModel>(userList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            var userBO = mapper.Map<UserBO>(model);
            userBO.Save();

            return RedirectToActionPermanent("Index", "User");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(id);
            user.Delete(id);

            return RedirectToActionPermanent("Index", "User");
        }

        public ActionResult _UsersOrders(int id)
        {
            var orders = DependencyResolver.Current.GetService<OrderBO>();
            var userOrders = orders.GetOrdersList().Where(o => o.UserId == id).ToList();
            var books = DependencyResolver.Current.GetService<BookBO>().GetBooksList();
            var authors = DependencyResolver.Current.GetService<AuthorBO>().GetAuthorsList();

            ViewBag.TopOrders = userOrders.Select(m => mapper.Map<OrderViewModel>(m)).ToList().Distinct().Take(5);
            ViewBag.Books = books.Select(m => mapper.Map<BookViewModel>(m)).ToList();
            ViewBag.Authors = authors.Select(m => mapper.Map<AuthorViewModel>(m)).ToList();

            return PartialView("Partial/_UsersOrders");
        }
    }
}
