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

        [Required(ErrorMessage = "Обязательно заполните поле")]
        public string FirstName { get; set; }

        [RegularExpression(@"[A-Za-z]@")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина строки не менее 2х символов и не более 20")]
        public string LastName { get; set; }

    }
}
