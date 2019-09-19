using AutoMapper;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Attempt4.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Обязательно заполните поле")]
        public string FirstName { get; set; }
        //[RegularExpression(@"[A-Za-z]@")]
        //[StringLength(10, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3х символов и не более 10")]
        public string LastName { get; set; }

    }
}
