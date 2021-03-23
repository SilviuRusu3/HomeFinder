using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class SQLAttributeRepository : IAttributesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQLAttributeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public LocationAttributes AddAtribute(LocationAttributes attribute)
        {
            bool foundRank = false;
            foreach (var item in dbContext.Attributes)
            {
                if (item.Rank == attribute.Rank)
                {
                    foundRank = true;
                }
            }
            if (foundRank)
            {//if the rank of the new item is found in the database
                foreach (var item in dbContext.Attributes)
                {
                    if(item.Rank >= attribute.Rank)
                    {
                        item.Rank++;
                    }
                }
            }
            dbContext.Attributes.Add(attribute);
            dbContext.SaveChanges();
            return attribute;
        }

        public void DeleteAttribute(int id)
        {
            LocationAttributes deletedAttribute = dbContext.Attributes.Find(id);
            if (deletedAttribute != null)
            {
                foreach (var item in dbContext.Attributes)
                {
                    if (item.Rank > deletedAttribute.Rank)
                    {//Every rank grater than the deleted rank will be decreased
                        item.Rank--;
                    }
                }
                dbContext.Attributes.Remove(deletedAttribute);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<LocationAttributes> GetAllAttributes(string userId)
        {
            return dbContext.Attributes.Where(a => a.UserId == userId);
        }

        public LocationAttributes GetAttribute(int Id)
        {
            return dbContext.Attributes.Find(Id);
        }

        public void UpdateAttribute(LocationAttributes attributeChanges)
        {
            if(dbContext.Attributes.Find(attributeChanges.Id) != null)
            {
                int scarceRank = 0;
            foreach (var item in dbContext.Attributes)
            {
                if (item.Id == attributeChanges.Id)
                {
                    scarceRank = item.Rank;
                    //scareRank saves the value of the rank before change
                    item.Rank = attributeChanges.Rank;
                    item.Name = attributeChanges.Name;
                }
            }
            foreach (var item in dbContext.Attributes)
            {
                if (item.Rank == attributeChanges.Rank && item.Id != attributeChanges.Id)
                {//The item that has the value for the rank identical to scarce rank but does not have the same id
                //will have a changed rank
                    item.Rank = scarceRank;
                }
            }
            }
            dbContext.SaveChanges();
        }
    }
}
