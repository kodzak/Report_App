using System.Collections.Generic;
using ReportApp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using ReportApp.WebUI.Validators;
using ReportApp.Domain.Validators;

namespace ReportApp.WebUI.Models
{
    public class EventsListViewModel
    {
       // public IEnumerable<Event> Events { get; set; }
        public int EventID { get; set; }
        [Display(Name = "Nazwa Sprawozdania")]
        [Required]
        public string NazwaSpraw { get; set; }
        [Display(Name = "Imie i nazwisko prowadzacego")]
        public string ProwadzacyID { get; set; }
        [Display(Name = "Nazwa przedmiotu")]
        public string NazwaPrzedmiotu { get; set; }
 
        public string Instrukcja { get; set; }
        [Display(Name = "Początek wydarzenia"), DataType(DataType.Date)]
        [DateStart]
        [Required]
        public DateTime DataPoczatku { get; set; }
        [Display(Name = "Koniec wydarzenia")]
        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThanAttribute("DataPoczatku")]  
        public DateTime DataKonca { get; set; }
        [Display(Name = "Grupa")]
        [Required]
        public int EventforGroup { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

}
