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

        public IActionResult Details(int Id)
        {
            Notice notice = NoticeDAO.ShowDetail(Id);
            return View(notice);
        }

        public IActionResult Create()
        {
            return View("CreateView");
        }

        public IActionResult Edit(int Id)
        {
            Notice notice = NoticeDAO.ShowDetail(Id);
            return View("Update",notice);
        }

        public IActionResult Delete(int Id)
        {
            NoticeDAO.Delete(Id);
            var noticeli = NoticeDAO.GetNotices();
            return View("Index", noticeli);
        }


        public IActionResult ProcessCreate(Notice notice)
        {
            NoticeDAO.CreateNotice(notice);
            var noticeli = NoticeDAO.GetNotices();
            return View("Index", noticeli);
        }

        public IActionResult ProcessEdit(Notice notice)
        {
            NoticeDAO.UpdateNotice(notice);
            var noticeli = NoticeDAO.GetNotices();
            return View("Index", noticeli);
        }

        public ActionResult GetNotice(int id)
        {
            var noticeli = NoticeDAO.GetNotices();
            return Json(noticeli);
        }
    }
}
