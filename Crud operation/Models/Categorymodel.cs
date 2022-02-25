using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud_operation.Models
{
    public class Categorymodel
    {
       
        public Categorymodel()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Remark { get; set; }

       
        public virtual ICollection<Product> Products { get; set; }
    }
}