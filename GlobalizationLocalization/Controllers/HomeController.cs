using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalizationLocalization.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace GlobalizationLocalization.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;
        private readonly IStringLocalizer<HomeController> specificLocalizer;
        public HomeController(IStringLocalizer<SharedResource> sharedLocalizer,
                              IStringLocalizer<HomeController> specificLocalizer)
        {
            this.sharedLocalizer = sharedLocalizer;
            this.specificLocalizer = specificLocalizer;
        }
        public IActionResult Index()
        {
            ViewBag.TranslatedInfo = specificLocalizer["dependency"];
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
