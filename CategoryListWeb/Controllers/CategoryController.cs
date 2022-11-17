using CategoryListWeb.Data;
using CategoryListWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryListWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        //Category/Create        
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannnot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Category/Edit        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannnot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["Success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
       
        public IActionResult Delete(int? id)
        {             
            var categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(categoryFromDb);
                _db.SaveChanges();
                TempData["Success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
