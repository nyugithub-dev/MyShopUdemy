using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        //ProductCategoryRepository context;
        //InMemoryRepository<ProductCategory> context;
        IIRepository<ProductCategory> context;

        public ProductCategoryManagerController(IIRepository<ProductCategory> productContext)
        {
            //context = new InMemoryRepository<ProductCategory>();
            context = productContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);
            }
            else
            {
                context.Insert(productCategories);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategories = context.Find(Id);

            if (productCategories == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategories);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategories, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategories);
                }
                else
                {
                    productCategoryToEdit.Category = productCategories.Category;

                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}