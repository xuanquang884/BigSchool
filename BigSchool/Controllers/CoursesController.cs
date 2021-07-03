using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
       
        // GET: Courses
        public ActionResult Create()
        {
            Model1 db = new Model1();
            //get list category
            Course objCourse = new Course();
            objCourse.ListCategory = db.Category.ToList();
            return View(objCourse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            Model1 db= new Model1();

            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = db.Category.ToList();
                return View("Create", objCourse);
            }

            // lấy login user id
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;

            //add vào csdl
            db.Course.Add(objCourse);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}