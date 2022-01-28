using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Movie.Controllers
{
    public class HelloWordController : Controller
    {
       
        public IActionResult Index()
        {
            return View(); //визуализирует представление в ответ

        }

        
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
