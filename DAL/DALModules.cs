﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BLL.Entities;
using DAL.Context;
using DAL.Finder;
using Microsoft.EntityFrameworkCore;

namespace DAL.AutofacModules
{
    public class DALModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Book>>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Finder"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
