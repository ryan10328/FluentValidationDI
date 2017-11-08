using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidationDI.FluentValidation;
using FluentValidationDI.Validators;

namespace FluentValidationDI
{
    public static class AutofacConfig
    {
        public static void Bootstrap()
        {
            var assemblies = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();

            builder.RegisterControllers(assemblies);

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterFilterProvider();

            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(x => x.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Register custom validator factory
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new MyValidatorFactory(container);
            });
        }
    }
}