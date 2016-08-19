using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportApp.Domain.Abstract;
using ReportApp.Domain.Entities;

namespace ReportApp.Domain.Concrete
{

    public class EFEventRepository : IEventRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Event> Events
        {
            get { return context.Events; }
        }
        public Event Find(int id){
           var find=context.Events.Single(a=>a.EventID==id);
        return find;
        }
    }
}