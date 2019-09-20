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
    public class OrderController : Controller
    {
        // GET: Order
        protected IMapper mapper;

        public OrderController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderViewModel>(m)).ToList();
            ViewBag.Users = userBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList();
            ViewBag.Books = bookBO.GetBooksList().Select(m => mapper.Map<BookViewModel>(m)).ToList();

            return View();
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var model = mapper.Map<OrderViewModel>(orderBO);

            if (id != null)
            {
                var orderBOList = orderBO.GetOrdersListById(id);
                model = mapper.Map<OrderViewModel>(orderBOList);
                ViewBag.Message = "Edit";
            }
            else ViewBag.Message = "Create";

            ViewBag.Users = new SelectList(userBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList(), "Id", "FIO");
            ViewBag.Books = new SelectList(bookBO.GetBooksList().Select(m => mapper.Map<BookViewModel>(m)).ToList(), "Id", "Title");

            return View(model);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderViewModel model)
        {
            var orderBO = mapper.Map<OrderBO>(model);
            orderBO.CreationDate = DateTime.Today;
            orderBO.Deadline = DateTime.Today;
            orderBO.ReturnDate = DateTime.Today;
            orderBO.Save();

            return RedirectToActionPermanent("Index", "Order");
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            orderBO.Delete(id);

            return RedirectToActionPermanent("Index", "Order");
        }
    }
}
