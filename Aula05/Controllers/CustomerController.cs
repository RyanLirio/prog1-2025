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

            SaveFile(fileContent, "DelimitatedFile.txt");
            
            return View();
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent +=
                    String.Format("{0:5}", c.Id) +
                    String.Format("{0:64}", c.Name) +
                    String.Format("{0:5}", c.HomeAddress!.Id) +
                    String.Format("{0:32}", c.HomeAddress!.City) +
                    String.Format("{0:2}", c.HomeAddress!.StateProvince) +
                    String.Format("{0:32}", c.HomeAddress!.Country) +
                    String.Format("{0:64}", c.HomeAddress!.StreetLine1) +
                    String.Format("{0:64}", c.HomeAddress!.StreetLine2) +
                    String.Format("{0:9}", c.HomeAddress!.PostalCode) +
                    String.Format("{0:16}", c.HomeAddress!.AddressType) +
                    "\n";
            }
            SaveFile(fileContent, "FixedFile.txt");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Customer customer = _customerRepository.Retrieve(id.Value);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_customerRepository.DeleteById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            bool ret = true;

            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                return false;

            var path = Path.Combine(
                environment.WebRootPath,
                "TextFiles"
            );

            try
            {
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                var filepath = Path.Combine(
                    environment.WebRootPath,
                    path,
                    fileName
                );

                if (!System.IO.File.Exists(filepath))
                {
                    using (StreamWriter sw = System.IO.File.CreateText(filepath))
                    {
                        sw.Write(content);
                    }

                }
            }
            catch (IOException ioEx)
            {
                string msg = ioEx.Message;
                ret = false;
                //throw ioEx;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
            }
            return ret;
        }
    }
}
