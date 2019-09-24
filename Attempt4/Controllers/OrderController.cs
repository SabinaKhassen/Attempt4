using Attempt4.ViewModels;
using AutoMapper;
using BussinessLayer.BussinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public ActionResult Index(string sort)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>();
            var userBO = DependencyResolver.Current.GetService<UserBO>();
            var bookBO = DependencyResolver.Current.GetService<BookBO>();

            ViewBag.Users = userBO.GetUsersList().Select(m => mapper.Map<UserViewModel>(m)).ToList();
            ViewBag.Books = bookBO.GetBooksList().Select(m => mapper.Map<BookViewModel>(m)).ToList();

            if (Request.IsAjaxRequest())
            {
                if (sort == "Creation Date")
                {
                    var orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderViewModel>(m)).ToList();
                    ViewBag.Orders = orders.OrderBy(o => o.CreationDate);
                }
                else if (sort == "None")
                    ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderViewModel>(m)).ToList();
                return PartialView("Partial/OrderPartialView");
            }
            else
            {
                ViewBag.Orders = orderBO.GetOrdersList().Select(m => mapper.Map<OrderViewModel>(m)).ToList();
            }

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
            if (model.Id == 0)
            {
                var allow = orderBO.GetOrdersList().Select(m => mapper.Map<OrderViewModel>(m)).Where(o => o.UserId == model.UserId).ToList();
                var list = allow.Where(a => a.Deadline < DateTime.Today && a.CreationDate == a.ReturnDate).ToList();

                if (list.Count == 0)
                {
                    orderBO.CreationDate = DateTime.Today;
                    if (model.ReturnDate == null) orderBO.ReturnDate = orderBO.CreationDate;
                    orderBO.Save();
                }
            }
            else
            {
                if (model.ReturnDate == null) orderBO.ReturnDate = orderBO.CreationDate;
                orderBO.Save();
            }

            return RedirectToActionPermanent("Index", "Order");
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            orderBO.Delete(id);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult SendEmail(int id)
        {
            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersListById(id);
            var order = mapper.Map<OrderViewModel>(orderBO);
            var bookBO = DependencyResolver.Current.GetService<BookBO>().GetBooksListById(order.BookId);
            var titleBook = mapper.Map<BookViewModel>(bookBO);
            var userBO = DependencyResolver.Current.GetService<UserBO>().GetUsersListById(order.UserId);
            var userMail = mapper.Map<UserViewModel>(userBO);

            MailAddress from = new MailAddress("sabina.khasen@gmail.com", "Sabina");
            MailAddress to = new MailAddress(userMail.Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Return '" + titleBook.Title + "'";
            message.Body = string.Format("Your order was due to " + order.Deadline + ". Return the book!");
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("sabina.khasen@gmail.com", "asgbdn19737577");
            smtp.EnableSsl = true;
            smtp.Send(message);

            return RedirectToActionPermanent("Index", "Order");
        }

        public ActionResult FileDeadline()
        {
            List<OrderViewModel> deadlines = new List<OrderViewModel>();

            var orderBO = DependencyResolver.Current.GetService<OrderBO>().GetOrdersList();
            var userBO = DependencyResolver.Current.GetService<UserBO>();

            deadlines = orderBO.Select(m => mapper.Map<OrderViewModel>(m)).Where(o => o.CreationDate == o.ReturnDate && DateTime.Today > o.Deadline).ToList();
            string path = @"C:\Test\deadline.txt";

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode))
                {
                    foreach (var item in deadlines)
                    {
                        var user = mapper.Map<UserViewModel>(userBO.GetUsersListById(item.UserId));
                        string fio = user.FIO;
                        sw.WriteLine($"User: {fio}   CreationDate: {item.CreationDate}  Deadline: {item.Deadline}");
                    }
                }
            }

            #region MemoryStream
            //    //byte[] data = new byte[5000];
            //    //MemoryStream ms = new MemoryStream(data);
            //    //StreamWriter sw = new StreamWriter(ms);

            //    //foreach (var item in links)
            //    //    if (item.Deadline < DateTime.Now)
            //    //    {
            //    //        string fio = db.Users.Where(u => u.Id == item.UserId).FirstOrDefault().FIO;
            //    //        sw.WriteLine($"User: {fio}   CreationDate: {item.CreationDate}  Deadline: {item.Deadline}");
            //    //    }
            //    //sw.Flush();
            //    //sw.Close();
            //    ////sr.Close();
            //    //string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //    //FileStream file = new FileStream($@"{dir}\test.txt", FileMode.OpenOrCreate);
            //    //ms.CopyTo(file);
            //    ////return File(ms, "text/plain");
            #endregion

            return RedirectToActionPermanent("Index", "Order");
        }

    }
}
