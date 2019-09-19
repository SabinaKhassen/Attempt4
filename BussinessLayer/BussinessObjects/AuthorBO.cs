using AutoMapper;
using DataLayer.Entities;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BussinessLayer.BussinessObjects
{
    public class AuthorBO : BussinessObjectBase<Authors>
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AuthorBO(IMapper mapper, UnitOfWorkFactory<Authors> unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public AuthorBO GetAuthorsListById(int? id)
        {
            AuthorBO authors;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                authors = unitOfWork.EntityRepository.GetAll().Where(a => a.Id == id).Select(item => mapper.Map<AuthorBO>(item)).FirstOrDefault();
            }
            return authors;
        }

        public List<AuthorBO> GetAuthorsList()
        {
            List<AuthorBO> authors = new List<AuthorBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                authors = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<AuthorBO>(item)).ToList();
            }
            return authors;
        }

        public void Save()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                if (Id != 0)
                    Update(unitOfWork);
                else
                    Add(unitOfWork);
            }
        }

        void Add(IUnitOfWork<Authors> unitOfWork)
        {
            var author = mapper.Map<Authors>(this);
            unitOfWork.EntityRepository.Add(author);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Authors> unitOfWork)
        {
            var author = mapper.Map<Authors>(this);
            unitOfWork.EntityRepository.Update(author);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.EntityRepository.Delete(id);
                unitOfWork.Save();
            }
        }
    }
}
