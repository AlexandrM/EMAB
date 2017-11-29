using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMAB.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}