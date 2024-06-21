using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2_implement_Theme.Models;

namespace Project2_implement_Theme.Controllers
{
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: Student
        public ActionResult Index()
        {
            return View(db.StudentInfoes.ToList());
        }


        [HttpGet]
        [Route("CreateOrEdit/{id:int}")]
        // GET: Student/CreateOrEdit
        // GET: student/CreateOrEdit/5
        public ActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return PartialView("_Create", new StudentInfo());
            }
            else
            {
                StudentInfo studentInfo = db.StudentInfoes.Find(id);
                if (studentInfo == null)
                {
                    return HttpNotFound("Incomplete Details.");
                }
                return PartialView("_Create", studentInfo);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateOrEdit/{id}")]
        // POST: Student/CreateOrEdit/5
        // POST: Student/CreateOrEdit
        public ActionResult CreateOrEdit([Bind(Include = "id,Name,City,PinCode")] StudentInfo studentInfo)
        {
            if (ModelState.IsValid)
            {
                if (studentInfo.id == 0)
                {
                    db.StudentInfoes.Add(studentInfo); //create
                    db.SaveChanges();
                    return Json(new { success = true, message = "Student Created successfully." });
                }
                else
                {
                    db.Entry(studentInfo).State = EntityState.Modified; // edit
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data updated successfully." });
                }
               
            }
            return PartialView("_Create", studentInfo);
        }

        [HttpGet]
        [Route("Delete/{id:int}")]
        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid student ID.");
            }

            var studentInfo = db.StudentInfoes.Find(id);
            if (studentInfo == null)
            {
                return HttpNotFound("Student not found.");
            }

            return PartialView("_Delete", studentInfo);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        // POST: Student/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid student ID." });
            }

            var studentInfo = db.StudentInfoes.Find(id);
            if (studentInfo == null)
            {
                return Json(new { success = false, message = "Student not found." });
            }

            db.StudentInfoes.Remove(studentInfo);
            db.SaveChanges();

            return Json(new { success = true, message = "Student deleted successfully." });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
