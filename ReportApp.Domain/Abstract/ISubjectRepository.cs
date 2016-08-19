using ReportApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Domain.Abstract
{
    public interface ISubjectRepository
    {
        IQueryable<Subjects> Subjects { get; }
        Subjects Find(int id);
        void Add(Subjects sub);
        void Save();
       
    }
}
