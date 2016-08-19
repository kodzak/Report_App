using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Domain.Entities
{
     [Table("Subjects")]
    public class Subjects
    {
        [Key]
         public int PrzedmiotID { get; set; }
        [Display(Name = "Nazwa przedmiotu")]
        public string Nazwa { get; set; }

    }
}
