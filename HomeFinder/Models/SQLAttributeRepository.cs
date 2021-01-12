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
            {
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
                    {
                        item.Rank--;
                    }
                }
                dbContext.Attributes.Remove(deletedAttribute);
                dbContext.SaveChanges();
            }
            //return deletedAttribute;
        }

        public IEnumerable<LocationAttributes> GetAllAttributes()
        {
            return dbContext.Attributes;
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
                    item.Rank = attributeChanges.Rank;
                    item.Name = attributeChanges.Name;
                }
            }
            foreach (var item in dbContext.Attributes)
            {
                if (item.Rank == attributeChanges.Rank && item.Id != attributeChanges.Id)
                {
                    item.Rank = scarceRank;
                }
            }
            }
            dbContext.SaveChanges();
            //return attributeChanges;
        }
    }
}
