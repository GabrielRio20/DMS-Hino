using Microsoft.AspNetCore.Mvc;

namespace DMS_Hino.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return PartialView("DocumentView");
        }
    }
}
