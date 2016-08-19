using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Domain.Entities
{
     [Table("Lab_Group")]
    public class Lab_Group
    {
        [Key]
        public int Id { get; set; }

    }
}
