using Microsoft.AspNetCore.Mvc;
using Flight_Management_Full_Stack.Models;
namespace Flight_Management_Full_Stack.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind] Login ad)
        {
            var dbop = new db();
            var result = dbop.LoginCheck(ad);
            if (result == true)
            {
                TempData["msg"] = "You are welcome to Admin Section";
            }
            else
            {
                TempData["msg"] = "Admin id or Password is wrong.!";
            }
            return View();
        }
    }
}
