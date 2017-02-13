using System.Web.Mvc;

namespace RideSharePlus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Learn more about OC RideShare.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to contact OC RideShare.";

            return View();
        }
    }
}