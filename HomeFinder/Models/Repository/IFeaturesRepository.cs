using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public interface IFeaturesRepository
    {
        HomeFeatures GetFeature(int Id);
        IEnumerable<HomeFeatures> GetAllFeatures(string userId);

        HomeFeatures AddFeature(HomeFeatures feature);
        void UpdateFeature(HomeFeatures featureChanges);
        void DeleteFeature(int id);
    }
}
