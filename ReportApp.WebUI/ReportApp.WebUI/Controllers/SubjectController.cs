using ReportApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportApp.Domain.Entities;
using ReportApp.WebUI.Models;
using ReportApp.Domain.Concrete;
using System.Net;
using System.Data.Entity;

namespace ReportApp.WebUI.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        private ISubjectRepository repositorysub;
        private EFDbContext context = new EFDbContext();
        public SubjectController(ISubjectRepository SubjectRepository)
        {
            this.repositorysub = SubjectRepository;
        }
         [Authorize(Roles = "Administrator, Prowadzący")]
        public ActionResult List()
        {
            return View(context.Subjects.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Subjects sub)
        {
            if (context.Subjects.Any(o => o.Nazwa == sub.Nazwa))
            {
                ViewData["Message"] = "Taki przedmiot już istnieje!";
                return View();
            }
            else
            context.Subjects.Add(sub);
            context.SaveChanges();
            return RedirectToAction("CreateEvent", "Event");
               
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects subjects = context.Subjects.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrzedmiotID,Nazwa")] Subjects subjects)
        {
            if (ModelState.IsValid)
            {
                context.Entry(subjects).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subjects);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects subjects = context.Subjects.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subjects subjects = context.Subjects.Find(id);
            context.Subjects.Remove(subjects);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
