using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //ProductRepository context;
        //InMemoryRepository<Product> context;
        IIRepository<Product> context;
        //ProductCategoryRepository productCategories;
        IIRepository<ProductCategory> productCategories;

        public HomeController(IIRepository<Product> productContext,
             IIRepository<ProductCategory> productCategoryContext)
        {
            //context = new ProductRepository();
            //context = new InMemoryRepository<Product>();
            context = productContext;
            //context = new ProductRepository();
            //productCategories = new InMemoryRepository<ProductCategory>();
            productCategories = productCategoryContext;
        }
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}