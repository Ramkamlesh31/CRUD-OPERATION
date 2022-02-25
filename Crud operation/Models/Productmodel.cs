using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_operation.Models
{
    public class Productmodel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ReorderLevel { get; set; }
        public int CatId { get; set; }

        public virtual Category Category { get; set; }
    }
}