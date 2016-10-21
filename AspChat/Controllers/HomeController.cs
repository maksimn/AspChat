using System.Web.Mvc;
using AspChat.Services;

namespace AspChat.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            IHomeService service = new HomeService(HttpContext);
            var viewModel = service.GetIndexViewModel();
            return View(viewModel);
        }
	}
}