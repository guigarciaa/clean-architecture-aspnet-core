using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.MVC.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        public readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productAppService)
        {
            _logger = logger;
            _productService = productAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetProducts();
            return View(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        // [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Price")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) return NotFound();

            var productVM = await _productService.GetById(id);

            if(productVM == null) return NotFound();

            return View(productVM);
        }

        [HttpPost("Edit")]
        public IActionResult Edit([Bind("Id,Name,Description,Price")] ProductViewModel productVM) {
            if(ModelState.IsValid) {
                try {
                    _productService.Update(productVM);
                }
                catch (Exception) {
                    throw;
                }
            }
            return View(productVM);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var productVM = await _productService.GetById(id);

            if (productVM == null) return NotFound();

            return View(productVM);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productVM = await _productService.GetById(id);

            if (productVM == null) return NotFound();

            return View(productVM);
        }

        [HttpPost("Delete"), ActionName("Delete")]
        public IActionResult DeleteConfirmed([Bind("Id,Name,Description,Price")] ProductViewModel productVM) {
            _productService.Remove(productVM);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}