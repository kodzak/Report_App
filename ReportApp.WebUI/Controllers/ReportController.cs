using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportApp.Domain.Abstract;
using ReportApp.Domain.Entities;
using ReportApp.WebUI.Models;
using ReportApp.Domain.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReportApp.WebUI.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository repository;
        private EFDbContext context = new EFDbContext();
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public int PageSize = 10;
        public ReportController(IReportRepository reportRepository)
        {
            this.repository = reportRepository;
            ApplicationDbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext));
        }
        public ViewResult List(string PrzedmiotID,int page = 1)
        {
            ReportsListViewModel viewModel;
            if(!String.IsNullOrEmpty(PrzedmiotID))
            {
                int id=Int32.Parse(PrzedmiotID);
                viewModel = new ReportsListViewModel
                {
                    Reports = repository.Reports.Where(a=>a.PrzedmiotID==id)
                    .OrderBy(p => p.PrzedmiotID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Reports.Count()
                    }
                };
            }
            else { 
             viewModel = new ReportsListViewModel
            {
                Reports = repository.Reports
                .OrderBy(p => p.PrzedmiotID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                   TotalItems = repository.Reports.Count()
                }
                };
            }
            IEnumerable<SelectListItem> items = context.Subjects
            .Select(c => new SelectListItem
            {
                Value = c.PrzedmiotID.ToString(),
                Text = c.Nazwa
            });
            ViewBag.PrzedmiotID = items;
            
            return View(viewModel);
        }
         [Authorize(Roles = "Prowadzący, Administrator")]
        public ViewResult Ocen(int id)
        {

            var raport = context.Reports.SingleOrDefault(a => a.SprawID == id);

            return View(raport);
        }
        [HttpPost]
        [Authorize(Roles = "Prowadzący, Administrator")]
        public ActionResult Ocen(Report report)
        {
           
            var repo = context.Reports.Single(p => p.SprawID == report.SprawID);
            repo.Ocena = report.Ocena;
            context.SaveChanges();
            return RedirectToAction("Details","Event",new {id=report.EventID});
        }
        public string nazwauzytkownika(string id)
        {
            var user = UserManager.FindById(id);
            string nazwa = user.FirstName + " " + user.LastName;
            return nazwa;
        }
        public ViewResult OcenList()
        {
            var UserID = UserManager.FindByName(User.Identity.Name).Id;
            var lista = context.Reports.ToList().OrderBy(a=>a.PrzedmiotID).Where(a => a.UserID == UserID);

            return View(lista);
        }
        public string NazwaPrzedmiotu(int id)
        {
            var nazwa = context.Subjects.FirstOrDefault(p => p.PrzedmiotID == id).Nazwa;
            return nazwa;
        }
        public string Numergrupy_stanow(string id)
        {
            var user = UserManager.FindById(id);
            string nazwa = "Grupa " + user.Nr_grupy.ToString() + " Stanowisko " + user.Nr_stanowiska.ToString();
            return nazwa;
        }
        public string nazwasprawozdania(int id)
        {
            var nazwa = context.Events.Find(id);
            var events = nazwa.NazwaSpraw;
            return events;
        }
    }

}