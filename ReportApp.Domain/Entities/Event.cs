using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReportApp.WebUI.Validators;
using ReportApp.Domain.Validators;


namespace ReportApp.Domain.Entities
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [Display(Name = "Nazwa Sprawozdania")]
        [Required]
        public string NazwaSpraw { get; set; }
         [Display(Name = "Imie i nazwisko prowadzacego")]
        public string ProwadzacyID { get; set; }
         [Display(Name = "Nazwa przedmiotu")]
        public int PrzedmiotID { get; set; }
       //[ValidateFile(ErrorMessage = "Proszę wybrać plik")]
        
        public string Instrukcja { get; set; }
        [Display(Name = "Początek wydarzenia"), DataType(DataType.Date)]
        [Required]
        [DateStart]
        public DateTime DataPoczatku { get; set; }
        [Display(Name = "Koniec wydarzenia")]
        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThanAttribute("DataPoczatku")]  
        public DateTime DataKonca { get; set; }
         [Display(Name = "Grupa")]
         [Required]
        public int EventforGroup { get; set; }
        public virtual List<Report> Reports { get; set; }

    }
}
