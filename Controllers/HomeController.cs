using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Notice_board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace Notice_board.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetNotice([FromBody] Pageinfo pageinfo)
        {
            var noticedao = new NoticeDAO();
            var noticeli = noticedao.GetList(pageinfo);
            var rownum = noticedao.Getcount(pageinfo);
            return Json(new {Noticelist = noticeli, total = rownum });
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] Newnotice newnotice)
        {
            var noticedao = new NoticeDAO();
            return Json(noticedao.Create(newnotice));
        }

        public ActionResult UpdateOne([FromBody] Notice notice)
        {
            var noticedao = new NoticeDAO();
            return Json(noticedao.Update(notice));
        }
        [HttpPost]
        public ActionResult DeleteItem([FromBody] Notice notice)
        {
            var noticedao = new NoticeDAO();
            return Json(noticedao.Delete(notice.id));
        }
    }
}