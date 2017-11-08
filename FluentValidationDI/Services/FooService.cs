using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluentValidationDI.Services
{
    public class FooService : IFooService
    {
        public int GetBar(int a)
        {
            return a * a;
        }
    }
}