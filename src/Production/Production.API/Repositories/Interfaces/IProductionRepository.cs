using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Production.API.Models;

namespace Production.API.Repositories.Interfaces
{
    public interface IProductionRepository
    {
        Task<Activity> AddWorkItem(Activity item);
        Task<Activity> UpdateWorkItem(Activity basket);
    }
       
    }
