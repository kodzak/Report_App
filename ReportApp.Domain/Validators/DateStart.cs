using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ReportApp.WebUI.Validators
{
    public sealed class DateStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart >= DateTime.Now.AddDays(-1));
        }
    }
}
