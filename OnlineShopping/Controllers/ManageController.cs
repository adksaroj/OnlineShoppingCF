using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class ManageController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}