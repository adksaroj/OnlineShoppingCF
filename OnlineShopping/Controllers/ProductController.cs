﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Models;
using OnlineShoppingDataAccess;
using Newtonsoft.Json;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class ProductController : Controller
    {

        public ActionResult Grid()
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                IEnumerable<Product> prods = dbContext.Products.ToList<Product>();
                return View(prods);
            }
        }

        // GET: Product
        public ActionResult Index()
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                IEnumerable<Product> prods = dbContext.Products.ToList<Product>();
                return View(prods);
            }
        }

        /// <summary>
        /// Method to search for products for a query
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>JSON array of all products whose name contains the words present in search query</returns>
        public JsonResult GetJson(string id)
        {
            var keys = id.Trim().Split(' ');

            List<Product> prods = new List<Product>();

            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                foreach (var query in keys)
                {

                    IEnumerable<Product> prodForQuery = dbContext.Products.Where(p => p.ProductName.Contains(query)).ToList();
                    if (prodForQuery != null)
                        prods.AddRange(prodForQuery);
                }
                var prodsJson = JsonConvert.SerializeObject(prods);
                return Json(prodsJson, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("admin"))]
        public ActionResult Add(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";

                    if (product.Image != null && product.Image.ContentType.Contains("image"))
                    {
                        string path = Server.MapPath("~/static/products");
                        string imageName = product.ProductId + "." + product.Image.ContentType.Substring(6);
                        fileName = Path.GetFileName(imageName);

                        string fullPath = Path.Combine(path, fileName);

                        product.Image.SaveAs(fullPath);

                    }

                    using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
                    {
                        Product prod = new Product();
                        prod.ProductId = product.ProductId;
                        prod.ProductName = product.ProductName;
                        prod.Cost = product.Cost;
                        prod.Category = product.Category;
                        prod.Description = product.Description;



                        prod.ImageName = fileName;

                        dbContext.Products.Add(prod);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                Product prod = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
                return View(prod);
            }
        }

        [Authorize(Roles = ("admin"))]
        public ActionResult Edit(int id)
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                Product prod = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
                ProductViewModel pvm = new ProductViewModel();
                if (prod != null)
                {
                    pvm.Id = prod.Id;
                    pvm.ProductId = prod.ProductId;
                    pvm.ProductName = prod.ProductName;
                    pvm.Cost = prod.Cost;
                    pvm.Category = prod.Category;
                    pvm.Description = prod.Description;

                }
                return View(pvm);
            }
        }

        [HttpPost]
        [Authorize(Roles = ("admin"))]
        public ActionResult Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = "";

                    if (product.Image != null && product.Image.ContentType.Contains("image"))
                    {
                        string path = Server.MapPath("~/static/products");
                        string imageName = product.ProductId + "." + product.Image.ContentType.Substring(6);
                        fileName = Path.GetFileName(imageName);

                        string fullPath = Path.Combine(path, fileName);

                        product.Image.SaveAs(fullPath);

                    }

                    using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
                    {
                        Product prod = dbContext.Products.Where(p => p.Id == product.Id).FirstOrDefault();
                        prod.ProductId = product.ProductId;
                        prod.ProductName = product.ProductName;
                        prod.Cost = product.Cost;
                        prod.Category = product.Category;
                        prod.Description = product.Description;


                        if (product.Image != null)
                            prod.ImageName = fileName;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }

        /// <summary>
        /// Deletes the product with the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to previous list view</returns>

        [Authorize(Roles = ("admin"))]
        public ActionResult Delete(int id)
        {
            using (OnlineShoppingContext dbContext = new OnlineShoppingContext())
            {
                Product prod = dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
                dbContext.Products.Remove(prod);
                dbContext.SaveChanges();
                return RedirectToAction("grid");
            }
        }
    }
}