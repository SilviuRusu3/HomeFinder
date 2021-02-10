using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public interface IAttributesRepository
    {
        LocationAttributes GetAttribute(int Id);
        IEnumerable<LocationAttributes> GetAllAttributes(string userId);

        LocationAttributes AddAtribute(LocationAttributes attribute);
        void UpdateAttribute(LocationAttributes attributeChanges);
        void DeleteAttribute(int id); 
    }
}
