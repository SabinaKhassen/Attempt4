using AutoMapper;
using DataLayer.Entities;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BussinessLayer.BussinessObjects
{
    public class UserBO : BussinessObjectBase<Users>
    {
        private readonly IUnityContainer unityContainer;

        public int Id { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }

        public UserBO(IMapper mapper, UnitOfWorkFactory<Users> unitOfWorkFactory, IUnityContainer unityContainer)
            : base(mapper, unitOfWorkFactory)
        {
            this.unityContainer = unityContainer;
        }

        public UserBO GetUsersListById(int? id)
        {
            UserBO users;

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                users = unitOfWork.EntityRepository.GetAll().Where(u => u.Id == id).Select(item => mapper.Map<UserBO>(item)).FirstOrDefault();
            }
            return users;
        }

        public List<UserBO> GetUsersList()
        {
            List<UserBO> users = new List<UserBO>();

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                users = unitOfWork.EntityRepository.GetAll().Select(item => mapper.Map<UserBO>(item)).ToList();
            }
            return users;
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

        void Add(IUnitOfWork<Users> unitOfWork)
        {
            var author = mapper.Map<Users>(this);
            unitOfWork.EntityRepository.Add(author);
            unitOfWork.Save();
        }

        void Update(IUnitOfWork<Users> unitOfWork)
        {
            var author = mapper.Map<Users>(this);
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
