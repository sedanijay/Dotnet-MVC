using InterviewTask.Models;
using InterviewTask.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class categoryController : Controller
    {
        // GET: category
        public ActionResult Index()
        {
            Category_Repo CatRepo = new Category_Repo();
            ModelState.Clear();
            return View(CatRepo.TotalRecords());
            //return View(CatRepo.GetAllCategories());
        }

        public ActionResult View(int id)
        {
            Category_Repo CatRepo = new Category_Repo();
            ModelState.Clear();
            return PartialView(CatRepo.GetAllCategories(id*5));
        }

        // GET: category/Create   
        public ActionResult Create()
        {
            return View();
        }

        // POST: category/Create
        [HttpPost]
        public ActionResult Create(categoryModel catModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category_Repo CatRepo = new Category_Repo();

                    if (CatRepo.AddCategory(catModel))
                    {
                        ViewBag.Message = "Category added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: category/Edit/5
        public ActionResult Edit(int id)
        {
            Category_Repo CatRepo = new Category_Repo();
            return View(CatRepo.EditViewCategory().Find(cat => cat.cat_id == id));
        }

        // POST: category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, categoryModel catModel)
        {
            try
            {
                Category_Repo catRepo = new Category_Repo();

                catRepo.UpdateCategory(catModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: category/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Category_Repo catRepo = new Category_Repo();
                if (catRepo.DeleteCategory(id))
                {
                    ViewBag.AlertMsg = "Category deleted successfully";

                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        
    }
}
