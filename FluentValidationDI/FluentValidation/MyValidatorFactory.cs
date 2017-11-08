using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using FluentValidation;

namespace FluentValidationDI.FluentValidation
{
    /// <inheritdoc />
    /// <summary>
    /// Custom validator factory
    /// </summary>
    public class MyValidatorFactory : IValidatorFactory
    {
        private readonly IComponentContext _container;

        // Constructor injection of Autofac DI container
        public MyValidatorFactory(IComponentContext container)
        {
            _container = container;
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            var genericType = typeof(IValidator<>).MakeGenericType(type);

            // Search if IValidator<> exists in Autofac container. If it exists then resolve it. Otherwise, return null.
            if (_container.TryResolve(genericType, out var validator))
                return (IValidator)validator;

            return null;
        }
    }
}