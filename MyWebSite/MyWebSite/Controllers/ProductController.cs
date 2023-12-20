namespace MyWebSite.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using static MyWebSite.Seeding.Seed;

    public class ProductController : Controller
    {
        [Route("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = Products
                    .FirstOrDefault(p => p.Name.ToLower().Contains(keyword));

                return View(foundProducts);
            }

            return View(Products);
        }

        public IActionResult ById(int id)
        {
            var product = Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return RedirectToAction("All");
            }

            return View(product);
        }

        public IActionResult AllAsJson(string name)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return Json(Products, options);
        }

        public IActionResult AllAsText()
        {

            var sb = new StringBuilder();
            foreach (var p in Products)
            {
                sb
                    .AppendLine($"Product {p.Id} {p.Name}")
                    .AppendLine($"{p.Price} lv.");
            }

            return Content(sb.ToString().TrimEnd());
        }

        public IActionResult AllAsTextFile()
        {
            var sb = new StringBuilder();

            foreach (var product in Products)
            {
                sb.AppendLine($"Products: {product.Id} {product.Name} - {product.Price:f2} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition,
                @"attachment;filename=product.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().Trim()), "text/plain");
        }
    }
}