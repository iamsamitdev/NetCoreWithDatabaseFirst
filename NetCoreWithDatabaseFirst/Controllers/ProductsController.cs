using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Load Model
using NetCoreWithDatabaseFirst.Models;
// Load EF Core
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCoreWithDatabaseFirst.Controllers
{
    public class ProductsController : Controller
    {
        private NorthwindContext db;

        public ProductsController(NorthwindContext ctx)
        {
            db = ctx;
        }

        public async Task<IActionResult> Index()
        {
            var ps = await (from p in db.Products
                            .Include(p => p.Category)
                            .Include(p => p.Supplier)
                            orderby p.ProductId
                            select p)
                            .ToListAsync();

            return View(ps);
        }


        public async Task<IActionResult> SearchProducts(string txtFilter)
        {
            if (string.IsNullOrEmpty(txtFilter))
            {
                return View("Index", await db.Products
                                                    .Include(p => p.Category)
                                                    .Include(p => p.Supplier)
                                                    .ToListAsync());
            }
            else
            {
                return View("Index", await db.Products
                                                    .Include(p => p.Category)
                                                    .Include(p => p.Supplier)
                                                    .Where(p => p.ProductName.Contains(txtFilter))
                                                    .ToListAsync());
            }
        }

        // Detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ps = await (from p in db.Products
                            .Include(p => p.Category)
                            .Include(p => p.Supplier)
                            select p)
                            .SingleOrDefaultAsync(p => p.ProductId == id);

            if (ps == null)
            {
                return NotFound();
            }

            return View(ps);
        }

        // Create Method
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "CompanyName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products products)
        {
            // มีข้อมูลใน model
            if (ModelState.IsValid)
            {
                db.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName", products.CategoryId);
                return View(products);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ps = await db.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (ps == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName", ps.CategoryId);

            ViewData["SupplierId"] = new SelectList(db.Suppliers, "SupplierId", "CompanyName");

           // ViewData["ProductId"] = id;

            return View(ps);
        }

        // Method Update 
        [HttpPost]
        public IActionResult Edit(int id, Products products)
        {
            products.ProductId = id;
            db.Products.Update(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // Method Delete
        [HttpPost]
        public IActionResult Delete(int id, Products products)
        {
            products.ProductId = id;
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
