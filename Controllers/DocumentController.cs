using Microsoft.AspNetCore.Mvc;

namespace DMS_Hino.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult PublicDocument()
        {
            return View("DocumentView_test");
        }
    }
}
