﻿using ReportApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Domain.Abstract
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }
        Event Find(int id);

    }
}
