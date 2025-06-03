using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;


namespace Aula05.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment environment;

        private CustomerRepository _customerRepository;

        public CustomerController(IWebHostEnvironment environment)
        {
            _customerRepository = new CustomerRepository();
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View(customers);
        }

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            _customerRepository.Save(c);
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View("Index", customers);
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
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;
            foreach(Customer c in CustomerData.Customers)
            {
                fileContent += 
                    $"{c.Id};\n{c.Name};\n{c.HomeAddress.Id};\n{ c.HomeAddress.City};\n{c.HomeAddress.StateProvince};\n{c.HomeAddress.Country};\n{c.HomeAddress.StreetLine1};\n{c.HomeAddress.StreetLine2};\n{c.HomeAddress.PostalCode};\n{c.HomeAddress.AddressType};";
            }

            var path = Path.Combine(
                environment.WebRootPath,
                "TextFiles"
            );

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var filepath = Path.Combine(
                environment.WebRootPath,
                path,
                "Delimitado.txt"
            );

            if (!System.IO.File.Exists(filepath))
            {
                using(StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.Write(fileContent);
                }
                
            }
            return View();
        }
    }
}
