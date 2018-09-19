using QuarterMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuarterMaster.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageHome()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult EditBanner()
        {

            if (User.IsInRole("Admin"))
            {


                var myHome = db.homePages.Select(h => h).FirstOrDefault();
                ViewBag.Picture1 = myHome.SliderPic1;
                ViewBag.Picture2 = myHome.SliderPic2;
                ViewBag.Picture3 = myHome.SliderPic3;
                return View();
                // after successfully uploading redirect the user

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }




        }
        [HttpPost]
        public ActionResult EditBanner1(HomePage info, HttpPostedFileBase file1)
        {
            var homePage = db.homePages.Select(h => h).FirstOrDefault();
            if (file1 != null)
            {
                string pic = System.IO.Path.GetFileName(file1.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content"), pic);
                file1.SaveAs(path);
                homePage.SliderPic1 = "/Content/" + pic;
            }
            db.SaveChanges();
            return RedirectToAction("EditBanner");

        }
        [HttpPost]
        public ActionResult EditBanner2(HomePage info, HttpPostedFileBase file2)
        {
            var homePage = db.homePages.Select(h => h).FirstOrDefault();
            if (file2 != null)
            {
                string pic = System.IO.Path.GetFileName(file2.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content"), pic);
                file2.SaveAs(path);
                homePage.SliderPic2 = "/Content/" + pic;
            }
            db.SaveChanges();
            return RedirectToAction("EditBanner");

        }
        [HttpPost]
        public ActionResult EditBanner3(HomePage info, HttpPostedFileBase file3)
        {
            var homePage = db.homePages.Select(h => h).FirstOrDefault();
            if (file3 != null)
            {
                string pic = System.IO.Path.GetFileName(file3.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content"), pic);
                file3.SaveAs(path);
                homePage.SliderPic3 = "/Content/" + pic;
            }
            db.SaveChanges();
            return RedirectToAction("EditBanner");
        }
    }
}