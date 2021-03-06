﻿using HomeFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public class SQLAreasRepository : IAreasRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SQLAreasRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Area AddArea(Area area)
        {
            dbContext.Areas.Add(area);
            dbContext.SaveChanges();
            return dbContext.Areas.Find(area.Id);
        }

        public Area addGradeToArea(int AreaId, double grade)
        {
            Area area = dbContext.Areas.Find(AreaId);
            area.GeneralGrade = grade;
            dbContext.SaveChanges();
            return area;
        }

        public double CalculateGrade(IList<LocationAttributes> locationAttributes)
        {
            double sum = 0, maxSum = 0;
            for(int i = 0; i< locationAttributes.Count; i++)
            {
                if (locationAttributes[i].Grade != null)
                {
                    if (locationAttributes[i].Grade > 0 && locationAttributes[i].Rank < 10)
                    {//Rank 1 will generate 1*grade; rank 9 will generate 0.3 * grade; rank above 9 is unusable so it is ignored
                        sum += Math.Log10(10 - locationAttributes[i].Rank + 1) * (double)locationAttributes[i].Grade;
                        //maxSum is calculated as if the grade is 10 for available ranks
                        maxSum += Math.Log10(10 - locationAttributes[i].Rank + 1);
                    }
                }
                
            }
            if (maxSum == 0)
            {
                return 0;
            }
            return Math.Round((sum / maxSum), 2);
        }

        public void DeleteArea(int id)
        {
            Area area = dbContext.Areas.Find(id);
            if (area != null)
            {
                dbContext.Areas.Remove(area);
                IEnumerable<AreaAttributes> areaAttributes = dbContext.AreaAttributes.Where(a => a.AreaId == id);
                dbContext.AreaAttributes.RemoveRange(areaAttributes);
                dbContext.SaveChanges();
            }
        }

        public void FillJointTable(LocationGrades locationGrades)
        {
            var areas = dbContext.AreaAttributes.Where(a => a.AreaId == locationGrades.AreaId);
            if (!areas.Any())//if the area hasn't allready been graded 
            {
                for (int i = 0; i < locationGrades.LocationAttributes.Count; i++)
                {
                    AreaAttributes areaAttributes = new AreaAttributes();
                    areaAttributes.AreaId = locationGrades.AreaId;
                    areaAttributes.LocationAttributesId = locationGrades.LocationAttributes[i].Id;
                    areaAttributes.Grade = locationGrades.LocationAttributes[i].Grade != null ? (double)locationGrades.LocationAttributes[i].Grade : areaAttributes.Grade = 0;
                    dbContext.AreaAttributes.Add(areaAttributes);
                    dbContext.SaveChanges();
                }
            } else
            {
                foreach (var loc in locationGrades.LocationAttributes)
                {
                    foreach (var a in areas)
                    {
                        if (a.LocationAttributesId == loc.Id)
                        {
                            a.Grade = loc.Grade != null ? (double)loc.Grade: 0;
                        }
                    }
                }
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Area> GetAllAreas(string userId)
        {
            return dbContext.Areas.Where(a => a.UserId == userId);
        }

        public Area GetArea(int Id)
        {
            return dbContext.Areas.Find(Id);
        }

        public void UpdateArea(Area areaChanges)
        {
            Area area = dbContext.Areas.Find(areaChanges.Id);
            area.Name = areaChanges.Name;
            dbContext.SaveChanges();
        }
    }
}
