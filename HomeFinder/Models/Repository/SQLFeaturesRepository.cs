using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public class SQLFeaturesRepository : IFeaturesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQLFeaturesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public HomeFeatures AddFeature(HomeFeatures feature)
        {
            dbContext.Features.Add(feature);
            dbContext.SaveChanges();
            return feature;
        }

        public void DeleteFeature(int id)
        {
            HomeFeatures deletedFeature = dbContext.Features.Find(id);
            if (deletedFeature != null)
            {
                dbContext.Features.Remove(deletedFeature);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<HomeFeatures> GetAllFeatures(string userId)
        {
            return dbContext.Features.Where(f => f.UserId == userId);
        }

        public HomeFeatures GetFeature(int Id)
        {
            return dbContext.Features.Find(Id);
        }

        public void UpdateFeature(HomeFeatures featureChanges)
        {
            var feature = dbContext.Features.Find(featureChanges.Id);
            feature.Name = featureChanges.Name;
            feature.HomeType = featureChanges.HomeType;
            dbContext.SaveChanges();
        }
    }
}
