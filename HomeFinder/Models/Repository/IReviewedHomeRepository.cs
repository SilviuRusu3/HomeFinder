using HomeFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public interface IReviewedHomeRepository
    {
        Home GetHome(int Id);
        IEnumerable<Home> GetAllHomes(int areaId);

        Home AddHome(Home home);
        Home AddGradeToHome(int homeId, double grade);
        void UpdateHome(Home homeChanges);
        void DeleteHome(int id);
        double CalculateGrade(IList<HomeFeatures> gradedFeatures);
        void FillJointTable(CreateHome gradedFeatures);
    }
}
