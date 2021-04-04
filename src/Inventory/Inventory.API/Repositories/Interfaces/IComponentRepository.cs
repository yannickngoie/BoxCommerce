using Inventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories.Interfaces
{
    public interface IComponentRepository
    {
        Task<Component> GetComponentById(Guid id, bool inStock = false);
        Task<IEnumerable<Component>> GetComponents();
        Task<Component> InsertComponent(Component component);
        Task<Component> UpdateComponent(Component component);
        Task DeleteComponent(Guid id);
    }
}
