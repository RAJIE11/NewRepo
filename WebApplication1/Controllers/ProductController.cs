using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _context; 

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET method to retrieve all products
        public IActionResult Index()
        {
            var products = _context.Products.ToList(); 
            return View(products); 
        }

        // GET method to retrieve a single product by ID
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        // GET method for creating a new product 
        public IActionResult Create()
        {
            return View(); 

        // POST method to create a new product 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            return View(product); 
        }

        // GET method for editing an existing product by ID
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); 
            }

            return View(product);
        }

        // POST method to update an existing product in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product); 
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    // Handle exceptions
                    return StatusCode(500, "Internal server error");
                }
                return RedirectToAction(nameof(Index)); 
            }
            return View(product); 
        }

        //  deletion of a product
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); 

            return View(product); 
        }

        // POST method to delete a product from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id== id);
            if (product == null)
            {
                return NotFound(); 
            }

            _context.Products.Remove(product); 
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
       
        public IActionResult Error()
        {
            return View();  
        }

    }
}
