using Crud_operation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Crud_operation.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Product( string q,string s)
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");


            //Searching Record  
            int id = Convert.ToInt32(Request["SearchType"]);
            var searchParameter = "Searching";
            var ProductWithCategoryVMlist = from P in db.Products
                                             join C in db.Categories on
              P.CatId equals C.CategoryId

                                             select new ProductWithCategoryVM
                                             {
                                                 ProductId = P.ProductId,
                                                 ProductName = P.ProductName,
                                                 Price = P.Price,
                                                 ReorderLevel = P.ReorderLevel,
                                                 CategoryName = P.Category.CategoryName
                                             };
            if (!string.IsNullOrWhiteSpace(q))
            {
                switch (id)
                {
                    case 0:
                        int iQ = int.Parse(q);
                        ProductWithCategoryVMlist = ProductWithCategoryVMlist.Where(p => p.ProductId.Equals(iQ));
                        searchParameter += " ProductId for ' " + q + " '";
                        break;
                    case 1:
                        ProductWithCategoryVMlist = ProductWithCategoryVMlist.Where(p => p.ProductName.Contains(q));
                        searchParameter += " Product Name for ' " + q + " '";
                        break;
                    case 2:
                        ProductWithCategoryVMlist = ProductWithCategoryVMlist.Where(p => p.CategoryName.Contains(q));
                        searchParameter += " Category Name for '" + q + "'";
                        break;
                }
            }
            else
            {
                searchParameter += "ALL";
            }

            ViewBag.SearchParameter = searchParameter;
            return View(ProductWithCategoryVMlist.ToList()); //List of Products with Category Name)  
            //return View(ProductWithCategoryVMlist); //List of Products with Category Name)  
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
            return View();
        }

        public ActionResult AllAbout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Insert()
        {

            try
            {
                string ProductName = Request["txtPName"].ToString();
                int CatId = Convert.ToInt32(Request["Categories"].ToString());
                decimal price = Convert.ToDecimal(Request["txtPrice"].ToString());
                int ReorderLevel = Convert.ToInt32(Request["txtReorderLevel"].ToString());
                int NextId = db.Products.Max(p => (int)p.ProductId) + 1;
                Product NewProduct = new Product();
                NewProduct.ProductId = NextId;
                NewProduct.ProductName = ProductName;
                NewProduct.CatId = CatId;
                NewProduct.Price = price;
                NewProduct.ReorderLevel = ReorderLevel;
                db.Products.Add(NewProduct);
                db.SaveChanges();
                //Sending Data To Products Controller.  
                TempData["Message"] = "Record saved successfully";
            }
            catch
            {
                TempData["Message"] = "Error while saving record";
            }
            return RedirectToAction("Products");

        }
        // GET: /Product/Edit/5  
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product editProduct = db.Products.Find(id);
          
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            if (editProduct == null)
            {
                return HttpNotFound();
            }
            return View(editProduct);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,CatId,Price,ReorderLevel")] Product editProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int CatId = Convert.ToInt32(Request["Categories"].ToString());
                    editProduct.CatId = CatId;
                    db.Entry(editProduct).State = EntityState.Modified;
                    db.SaveChanges();
                    editProduct = null;
                    TempData["Message"] = "Record updated successfully";
                    return RedirectToAction("Products");
                }
            }
            catch
            {
                TempData["Message"] = "Error while updating record";
            }
            return RedirectToAction("Products");

        }
        // GET: /Product/Delete/5  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product deleteProduct = db.Products.Find(id);
            if (deleteProduct == null)
            {
                return HttpNotFound();
            }
            return View(deleteProduct);
        }

        // POST: /Product/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product deleteProduct = db.Products.Find(id);
                db.Products.Remove(deleteProduct);
                db.SaveChanges();
                deleteProduct = null;
                TempData["Message"] = "Record Deleted successfully";
                return RedirectToAction("Products");

            }
            catch
            {
                TempData["Message"] = "Error while deleting record";
            }
            return RedirectToAction("Products");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}