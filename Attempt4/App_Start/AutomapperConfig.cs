using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using System.Web.Mvc;
using DataLayer.Entities;
using Attempt4.ViewModels;
using BussinessLayer.BussinessObjects;

namespace WebApplication2.App_Start
{
    public static class AutomapperConfig
    {
        public static void RegisterWithUnity(IUnityContainer container)
        {
            IMapper mapper = CreateMapperConfig().CreateMapper();

            container.RegisterInstance<IMapper>(mapper);
        }

        static MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Authors, AuthorBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, AuthorViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorViewModel>());

                cfg.CreateMap<AuthorViewModel, AuthorBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<AuthorBO>());

                cfg.CreateMap<AuthorBO, Authors>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Authors>());


                cfg.CreateMap<Books, BookBO>()//.ForMember(t=> t.Id, to => to.Ignore())
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, BookViewModel>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookViewModel>());

                cfg.CreateMap<BookViewModel, BookBO>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<BookBO>());

                cfg.CreateMap<BookBO, Books>()
                .ConstructUsing(item => DependencyResolver.Current.GetService<Books>());
            }
            );
        }

    }
}