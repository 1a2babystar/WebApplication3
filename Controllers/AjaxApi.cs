using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;

namespace WebApplication3.Controllers
{
    public class AjaxApi : Controller
    {
        public ActionResult GetNotice(int id)
        {
            var noticeli = NoticeDAO.GetNotices();
            return Json(noticeli);
        }
    }
}
