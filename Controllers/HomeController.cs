using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        public ActionResult GetOneNotice()
        {
            var reader = new System.IO.StreamReader(Request.Body);
            var body = reader.ReadToEndAsync().Result;
            var p = body.Substring(1, body.Length - 2);
            var obj = JObject.Parse(p);
            var id = Convert.ToInt32(obj["id"].ToString());
            var notice = NoticeDAO.ShowDetail(id);
            return Json(notice);
        }

        public ActionResult CreateNew()
        {
            var reader = new System.IO.StreamReader(Request.Body);
            var body = reader.ReadToEndAsync().Result;
            var p = body.Substring(1, body.Length - 2);
            var obj = JObject.Parse(p);
            var title = obj["title"].ToString();
            var content = obj["contents"].ToString();
            var writer = obj["writer"].ToString();
            Notice notice = new Notice();
            notice.title = title;
            notice.contents = content;
            notice.writer = writer;
            return Json(NoticeDAO.CreateNotice(notice));
        }

        public ActionResult UpdateOne()
        {
            var reader = new System.IO.StreamReader(Request.Body);
            var body = reader.ReadToEndAsync().Result;
            var p = body.Substring(1, body.Length - 2);
            var obj = JObject.Parse(p);
            var id = Convert.ToInt32(obj["id"].ToString());
            var title = obj["title"].ToString();
            var content = obj["contents"].ToString();
            var writer = obj["writer"].ToString();
            Notice notice = new Notice();
            notice.id = id;
            notice.title = title;
            notice.contents = content;
            notice.writer = writer;
            return Json(NoticeDAO.UpdateNotice(notice));
        }
        [HttpPost]
        public ActionResult DeleteItem()
        {
            var reader = new System.IO.StreamReader(Request.Body);
            var body = reader.ReadToEndAsync().Result;
            var p = body.Substring(1, body.Length-2);
            var obj = JObject.Parse(p);
            var id = Convert.ToInt32(obj["id"].ToString());
            return Json(NoticeDAO.Delete(id));
        }
    }
}