using ReportApp.Domain.Abstract;
using ReportApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Domain.Concrete
{
    public class EFSubjectRepository : ISubjectRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Subjects> Subjects
        {
            get { return context.Subjects; }
        }
        public Subjects Find(int id)
        {
            var find = context.Subjects.Single(a => a.PrzedmiotID == id);
            return find;
        }
        public void Add(Subjects sub)
        {
            context.Subjects.Add(sub);

        }
        public void Save()
        {
            context.SaveChanges();
        }
       
    }
}