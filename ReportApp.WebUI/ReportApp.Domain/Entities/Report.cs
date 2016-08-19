using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Domain.Entities
{
     [Table("Reports")]
    public class Report
    {
        [Key]
        [Display(Name = "Nazwa Sprawozdania")]
        public int SprawID { get; set; }
        [Display(Name = "Imie i nazwisko studenta")]
        public string UserID { get; set; }
        public int EventID { get; set; }
          [Display(Name = "Sprawozdanie")]
        public string File { get; set; }
         [Display(Name = "Nazwa przedmiotu")]
        public int PrzedmiotID { get; set; }
        public int Ocena { get; set; }
          [Display(Name = "Data dodania sprawozdania ")]
        public DateTime DataDodania { get; set; }
    }
}
