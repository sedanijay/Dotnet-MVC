using InterviewTask.Models;
using InterviewTask.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult Index()
        {
            Product_Repo ProdRepo = new Product_Repo();
            ModelState.Clear();
            return View(ProdRepo.TotalRecords());
        }

        public ActionResult View(int id)
        {
            Product_Repo temo_repo = new Product_Repo();
            ModelState.Clear();
            
            return PartialView(temo_repo.GetAllProducts(id*5));
            //return View(ProdRepo.GetAllProducts(offset));
        }


        // GET: product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: product/Create
        [HttpPost]
        public ActionResult Create(productModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product_Repo repo = new Product_Repo();

                    if (repo.AddProduct(obj))
                    {
                        ViewBag.Message = "Product added successfully";
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: product/Edit/5
        public ActionResult Edit(int id)
        {
            Product_Repo repo = new Product_Repo();
            return View(repo.EditViewProduct().Find(prod => prod.prod_id == id));
        }

        // POST: product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, productModel obj)
        {
            try
            {
                Product_Repo repo = new Product_Repo();

                repo.UpdateProduct(obj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Product_Repo repo = new Product_Repo();

                if (repo.DeleteProduct(id))
                {
                    ViewBag.AlertMsg = "Product deleted successfully";

                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetCategories()
        {
            Category_Repo repo = new Category_Repo();
            return Json(repo.EditViewCategory());

        }


    }
}
