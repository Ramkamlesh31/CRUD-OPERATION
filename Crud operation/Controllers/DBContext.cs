using Crud_operation.Models;
using System;
using System.Collections;

namespace Crud_operation.Controllers
{
    internal class DBContext
    {
        public IEnumerable Categories { get; internal set; }
        public object Products { get; internal set; }

        internal object Entry(Product editProduct)
        {
            throw new NotImplementedException();
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}