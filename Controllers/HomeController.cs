using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var noticeli = NoticeDAO.GetNotices();
            return View(noticeli);
        }
        public ActionResult GetNotice()
        {
            var noticeli = NoticeDAO.GetNotices();
            return Json(noticeli);
        }

        public ActionResult GetOneNotice(int id)
        {
            var notice = NoticeDAO.ShowDetail(id);
            return Json(notice);
        }

        public ActionResult CreateNew(string title, string content, string writer)
        {
            Notice notice = new Notice();
            notice.title = title;
            notice.contents = content;
            notice.writer = writer;
            return Json(NoticeDAO.CreateNotice(notice));
        }

        public ActionResult UpdateOne(int Id, string title, string content)
        {
            Notice notice = new Notice();
            notice.id = Id;
            notice.title = title;
            notice.contents = content;
            return Json(NoticeDAO.UpdateNotice(notice));
        }

        public ActionResult DeleteItem(int id)
        {
            return Json(NoticeDAO.Delete(id));
        }
    }
}
