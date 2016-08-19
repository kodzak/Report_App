using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ReportApp.Domain.Abstract;
using ReportApp.Domain.Entities;
using ReportApp.WebUI.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReportApp.Domain.Concrete;
using System.Data.Entity;


namespace ReportApp.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository repository;
        private IReportRepository report;
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private EFDbContext context = new EFDbContext();



        public int PageSize = 10;
        public EventController(IEventRepository EventRepository, IReportRepository ReportRepository)
        {
            this.repository = EventRepository;
            this.report = ReportRepository;

            ApplicationDbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext));
        }
        public string NazwaPrzedmiotu(int id)
        {
            var nazwa = context.Subjects.FirstOrDefault(p => p.PrzedmiotID == id).Nazwa;
            return nazwa;
        }
        public int GetUserGr(string name)
        {
            var id = UserManager.FindByName(name).Nr_grupy;
            return id;
        }
        public string nazwauzytkownika(string id)
        {
            var user = UserManager.FindById(id);
            string nazwa = user.FirstName + " " + user.LastName;
            return nazwa;
        }
        public ViewResult List(string PrzedmiotID, int page = 1)
        {
            IEnumerable<EventsListViewModel> Events;
            if (!String.IsNullOrEmpty(PrzedmiotID))
            {
                int id = Int32.Parse(PrzedmiotID);
                Events = context.Events.Where(a => a.PrzedmiotID == id).OrderBy(p => p.PrzedmiotID).Select(u => new EventsListViewModel()
                {
                    EventID = u.EventID,
                    ProwadzacyID = u.ProwadzacyID,
                    NazwaSpraw = u.NazwaSpraw,
                    NazwaPrzedmiotu = context.Subjects.FirstOrDefault(p => p.PrzedmiotID == u.PrzedmiotID).Nazwa,
                    EventforGroup = u.EventforGroup,
                    Instrukcja = u.Instrukcja,
                    DataPoczatku = u.DataPoczatku,
                    DataKonca = u.DataKonca

                }).ToList();
            }
            else
            {
                Events = context.Events.OrderBy(p => p.PrzedmiotID).Select(u => new EventsListViewModel()
                {
                    EventID = u.EventID,
                    ProwadzacyID = u.ProwadzacyID,
                    NazwaSpraw = u.NazwaSpraw,
                    NazwaPrzedmiotu = context.Subjects.FirstOrDefault(p => p.PrzedmiotID == u.PrzedmiotID).Nazwa,
                    EventforGroup = u.EventforGroup,
                    Instrukcja = u.Instrukcja,
                    DataPoczatku = u.DataPoczatku,
                    DataKonca = u.DataKonca

                }).ToList();
            }
                IEnumerable<SelectListItem> items = context.Subjects
           .Select(c => new SelectListItem
           {
               Value = c.PrzedmiotID.ToString(),
               Text = c.Nazwa
           });
                ViewBag.PrzedmiotID = items;
            
            return View(Events);
         }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var toView = repository.Find((int)id);

            if (toView == null) return RedirectToAction("Index");

            return View(toView);
        }
        //Tworzenie raportu dla danego wydarzenia
        [HttpPost]
       [Authorize]
        public ActionResult Create(Event wydarzenie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0 && file.ContentLength < 3000000 && DateTime.Today.Date <= wydarzenie.DataKonca && DateTime.Today.Date >= wydarzenie.DataPoczatku)
                {
                   
                    var uniqueFileName = nazwauzytkownika(UserManager.FindByName(User.Identity.Name).Id);
                    var sciezka = NazwaPrzedmiotu(wydarzenie.PrzedmiotID) + "/" + "Grupa " + wydarzenie.EventforGroup + "/" + wydarzenie.NazwaSpraw + "/";
                    var absolutePath = Server.MapPath("~/Files/" + sciezka);
                    if (!Directory.Exists(absolutePath))
                    {
                        Directory.CreateDirectory(absolutePath);
                    }
                    var folder = Path.Combine(absolutePath, uniqueFileName);
                    var relativePath = "~/Files/" + sciezka + uniqueFileName;
                    file.SaveAs(folder);
                    var sprawozdanie = new Report();
                    sprawozdanie.File = relativePath;
                    wydarzenie.EventforGroup = wydarzenie.EventforGroup;
                    sprawozdanie.DataDodania = DateTime.Now;
                    sprawozdanie.UserID = UserManager.FindByName(User.Identity.Name).Id;
                    sprawozdanie.EventID = wydarzenie.EventID;
                    sprawozdanie.PrzedmiotID = wydarzenie.PrzedmiotID;
                    sprawozdanie.Ocena = 1;
                    context.Reports.Add(sprawozdanie);
                    report.Add(sprawozdanie);
                    report.Save();
                }

                return RedirectToAction("Details", new { id = wydarzenie.EventID });
            }

            return Details(wydarzenie.EventID);

        }
       
        
        [Authorize(Roles = "Administrator, Prowadzący")]
        [HttpGet]
        public ActionResult CreateEvent()
        {
            IEnumerable<SelectListItem> items = context.Subjects
            .Select(c => new SelectListItem
            {
                Value = c.PrzedmiotID.ToString(),
                Text = c.Nazwa
            });
            ViewBag.PrzedmiotID = items;
            IEnumerable<SelectListItem> items2 = context.EventforGroup
           .Select(c => new SelectListItem
           {
               Value = c.Id.ToString(),
               Text = c.Id.ToString()
           });
            ViewBag.EventforGroup = items2;
          
            return View();
        }
     

        //Tworzenie wydarzenia
        [Authorize(Roles = "Administrator, Prowadzący")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(Event events, HttpPostedFileBase file)
        {
            IEnumerable<SelectListItem> items = context.Subjects
               .Select(c => new SelectListItem
               {
                   Value = c.PrzedmiotID.ToString(),
                   Text = c.Nazwa
               });
            ViewBag.PrzedmiotID = items;
            IEnumerable<SelectListItem> items2 = context.EventforGroup
           .Select(c => new SelectListItem
           {
               Value = c.Id.ToString(),
               Text = c.Id.ToString()
           });
            ViewBag.EventforGroup = items2;

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("", "Wybierz plik!");
                    return View();
                }
                if (file != null && file.ContentLength > 0 && file.ContentLength < 3000000)
                {
                   
                    var uniqueFileName = events.NazwaSpraw;
                    var sciezka = NazwaPrzedmiotu(events.PrzedmiotID) + "/" + "Grupa " + events.EventforGroup + "/";
                    var absolutePath = Server.MapPath("~/Files/" + sciezka + "Instrukcje do zajec/");
                    if (!Directory.Exists(absolutePath))
                    {
                        Directory.CreateDirectory(absolutePath);
                    }
                    var folder = Path.Combine(absolutePath, uniqueFileName);
                    var relativePath = "~/Files/" + sciezka + "Instrukcje do zajec/" + uniqueFileName;
                    file.SaveAs(folder);
                    events.ProwadzacyID = UserManager.FindByName(User.Identity.Name).Id;
                    events.NazwaSpraw = events.NazwaSpraw;
                    events.EventforGroup = events.EventforGroup;
                    events.Instrukcja = relativePath;
                    context.Events.Add(events);
                    context.SaveChanges();
                }
                else
                {
                    return View();
                }

                return RedirectToAction("List");
            }
            else
                return View();
        }
             
         public ActionResult Edit(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Event @event = context.Events.Find(id);
             if (@event == null)
             {
                 return HttpNotFound();
             }

             IEnumerable<SelectListItem> items = context.Subjects
             .Select(c => new SelectListItem
             {
                 Selected = (c.PrzedmiotID == @event.PrzedmiotID),
                 Value = c.PrzedmiotID.ToString(),
                 Text = c.Nazwa
             });
             ViewBag.PrzedmiotID = items;
             IEnumerable<SelectListItem> items2 = context.EventforGroup
            .Select(c => new SelectListItem
            {
                Selected = (c.Id == @event.EventforGroup),
                Value = c.Id.ToString(),
                Text = c.Id.ToString()
            });
             ViewBag.EventforGroup = items2;

            
             return View(@event);
         }

         // POST: Events/Edit/5
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Prowadzący")]
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Edit(Event events, HttpPostedFileBase file)
         {
             IEnumerable<SelectListItem> items = context.Subjects
              .Select(c => new SelectListItem
              {
                  Selected = (c.PrzedmiotID == events.PrzedmiotID),
                  Value = c.PrzedmiotID.ToString(),
                  Text = c.Nazwa
              });
             ViewBag.PrzedmiotID = items;
             IEnumerable<SelectListItem> items2 = context.EventforGroup
            .Select(c => new SelectListItem
            {
                Selected = (c.Id == events.EventforGroup),
                Value = c.Id.ToString(),
                Text = c.Id.ToString()
            });
             ViewBag.EventforGroup = items2;
             var editEvent = context.Events.FirstOrDefault(p => p.EventID == events.EventID);

             if (ModelState.IsValid)
             {
                 
                 if (file != null && file.ContentLength > 0 && file.ContentLength < 3000000)
                 {
                     var fileName = Path.GetFileName(file.FileName);
                     var uniqueFileName = events.NazwaSpraw + " " + fileName;
                     var sciezka = NazwaPrzedmiotu(events.PrzedmiotID) + "/" + "Grupa " + events.EventforGroup + "/";
                     var absolutePath = Server.MapPath("~/Files/" + sciezka + "Instrukcje do zajec/");
                     if (!Directory.Exists(absolutePath))
                     {
                         Directory.CreateDirectory(absolutePath);
                     }
                     var folder = Path.Combine(absolutePath, uniqueFileName);
                     var relativePath = "~/Files/" + sciezka + "Instrukcje do zajec/" + uniqueFileName;
                     file.SaveAs(folder);
                     editEvent.Instrukcja = relativePath;
                 }
                 if (editEvent.EventforGroup == events.EventforGroup)
                 {
                     editEvent.NazwaSpraw = events.NazwaSpraw;
                     editEvent.PrzedmiotID = events.PrzedmiotID;
                     editEvent.DataPoczatku = events.DataPoczatku;
                     editEvent.DataKonca = events.DataKonca;
                     editEvent.EventforGroup = events.EventforGroup;
                     context.SaveChanges();
                 }
                 else
                 {
                   var deletereports = context.Reports.Remove(context.Reports.FirstOrDefault(a => a.EventID == events.EventID));
                   editEvent.NazwaSpraw = events.NazwaSpraw;
                   editEvent.PrzedmiotID = events.PrzedmiotID;
                   editEvent.DataPoczatku = events.DataPoczatku;
                   editEvent.DataKonca = events.DataKonca;
                   editEvent.EventforGroup = events.EventforGroup;
                   context.SaveChanges();
                 }
                
             }
             else
                 return View(events);

             return RedirectToAction("List");
         }
        private void DeleteF(string relativePath)
        {
                var path = Server.MapPath(relativePath);
                System.IO.File.Delete(path);
           
        }
        [Authorize(Roles = "Administrator, Prowadzący")]
        public ActionResult DeleteFile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var toDelete = context.Events.Find(id);

            if (toDelete == null) return RedirectToAction("Index");

            DeleteF(toDelete.Instrukcja);
            toDelete.Instrukcja = String.Empty;
            context.SaveChanges();

           
            return RedirectToAction("Edit", new { id = toDelete.EventID });
        }

         // GET: Events/Delete/5
         public ActionResult Delete(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Event @event = context.Events.Find(id);
             if (@event == null)
             {
                 return HttpNotFound();
             }
             return View(@event);
         }

         // POST: Events/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             Event @event = context.Events.Find(id);
             context.Events.Remove(@event);
             context.SaveChanges();
             return RedirectToAction("List");
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