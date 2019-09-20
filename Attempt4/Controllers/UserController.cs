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
    }
}
