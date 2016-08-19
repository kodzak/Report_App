using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportApp.Domain.Abstract;

namespace ReportApp.WebUI.Controllers
{
    public class NavController : Controller
    {
        public string Menu()
        {
            return "Pozdrowienia z NavController";
        }
    }
}