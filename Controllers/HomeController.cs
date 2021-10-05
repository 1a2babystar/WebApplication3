using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var noticeli = Noticeacts.GetNotices();
            return View(noticeli);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Createhandler()
        {
            return View("create");
        }

        public IActionResult Createsubmit(string title, string writer, string content)
        {
            string time = DateTime.Now.ToString("HH:mm:ss tt");
            Notice notice = new Notice(title, writer, content, time);
            Noticeacts.CreateNotice(notice);
            var noticeli = Noticeacts.GetNotices();
            return View("index", noticeli);
        }

        public IActionResult Detail(int Id)
        {
            Notice notice = Noticeacts.ShowDetail(Id);
            return View(notice);
        }
    }
}
