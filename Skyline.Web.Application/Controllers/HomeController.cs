using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skyline.Web.Application.Controllers
{
    using Skyline.Services;
    using Skyline.Services.Abstract;

    public class HomeController : Controller
    {

        private readonly INewsService _newsService;

        public HomeController(INewsService newsService)
        {
            this._newsService = newsService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var test  = this._newsService.GetNews();
            return View();
        }
    }
}