using HomeFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public interface IAreasRepository
    {
        Area GetArea(int Id);
        IEnumerable<Area> GetAllAreas(string userId);
        Area addGradeToArea(int AreaId,double grade);

        Area AddArea(Area area);
        void UpdateArea(Area areaChanges);
        void DeleteArea(int id);
        double CalculateGrade(IList<LocationAttributes> locationAttributes);
        void FillJointTable(LocationGrades locationGrades);
    }
}
