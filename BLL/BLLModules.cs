using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BLL.Entities;
using BLL.Managers;
using BLL.Services;
using BLL.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace BLL.AutofacModules
{
    public class BLLModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Wrapper"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
