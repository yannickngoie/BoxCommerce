using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Production.API.Models;

namespace Production.API.Repositories.Interfaces
{
    public interface IProductionRepository
    {
        Task<List<Activity>> GetWorkItems();
        Task<IReadOnlyList<Activity>> GetItem(Expression<Func<Activity, bool>> predicate);
        Task<Activity> AddWorkItem(Activity item);      
        Task<Activity> UpdateWorkItem(Activity item);
        Task DeleteItem (string  item);
        Task<Activity> CompleteWorkItem(Activity item);
    }      
}
