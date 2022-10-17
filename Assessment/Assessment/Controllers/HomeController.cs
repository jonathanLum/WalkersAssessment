using Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Assessment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static bool Validate(string input, out int number)
        {
            int num;
            bool canParse = int.TryParse(input, out num);
            number = canParse ? num : 0;

            if (canParse && (1 <= num && num <= 200)) return true;

            return false;
        }

        [HttpPost]
        public ActionResult Index(string text, string number, string vis)
        {
            ViewBag.RawText = text;
            int visible = int.Parse(vis);

            if (text != number && text != "next 20") visible = 20;

            if (text == "next 20" && number != "-1")
            {
                visible += 20;
            }
            else
            {
                number = text;
            }
            
            Output output = new Output();
            output.Data = new List<List<string>>();

            int num;
            if (Validate(number, out num))
            {
                var day = DateTime.Now.DayOfWeek;

                for (int i = 1; i <= num; i++)
                {
                    List<string> line = new List<string>();

                    if (i % 3 == 0)
                    {
                        line.Add(day.ToString() != "Monday" ? "walkers" : "walkersm");
                    }
                    if (i % 5 == 0)
                    {
                        line.Add(day.ToString() != "Monday" ? "assessment" : "assessment-m");
                    }
                    if (!(i % 3 == 0 || i % 5 == 0))
                    {
                        line.Add($"{i}");
                    }

                    output.Data.Add(line);
                }
                ViewBag.Data = output.Data;
                ViewBag.Num = num;
                ViewBag.Visible = visible;
                return View("Index");
            }
            else
            {
                output.Data.Add(new List<string>() { "Invalid Input..." });
            }

            
            ViewBag.Data = output.Data;
            ViewBag.Visible = visible;
            return View("Index");
        }
    }
}