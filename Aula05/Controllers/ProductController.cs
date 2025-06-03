using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;

namespace Aula05.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment environment;

        private ProductRepository _productRepository;

        public ProductController(IWebHostEnvironment environment)
        {
            ProductRepository _productRepository = new();
            this.environment = environment;
        }

        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productRepository.RetrieveAll();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(Product c)
        {
            _productRepository.Save(c);
            List<Product> products = _productRepository.RetrieveAll();

            return View("Index", products);
        }

        [HttpPost]
        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportDelimitedFile()
        {
            string filecontent = string.Empty;
            foreach (Product c in ProductData.Products)
            {
                filecontent += $"{c.Id};\n{c.ProductName};\n{c.Description};\n{c.CurrentPrice};\n";
            }

            var path = Path.Combine(
                environment.WebRootPath,
                "TextFiles2"
            );

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var filepath = Path.Combine(
                environment.WebRootPath,
                path,
                "Delimitado2.txt"
            );

            if (!System.IO.File.Exists(filepath))
            {
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.Write(filepath);
                }

            }

            return View();
        }
    }
}
