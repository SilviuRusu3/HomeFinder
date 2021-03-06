﻿using HomeFinder.HelpClass;
using HomeFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models.Repository
{
    public class SQLReviewedHomeRepository : IReviewedHomeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SQLReviewedHomeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Home AddHome(Home home)
        {
            home.Id = 0;//this is done because model biding binds the same value for 2 items
            //home.AreaId and home.Id
            dbContext.Homes.Add(home);
            dbContext.SaveChanges();
            return home;
        }

        public void DeleteHome(int id)
        { 
            Home home = dbContext.Homes.Find(id);
            if (home != null)
            {
                dbContext.Homes.Remove(home);
                IEnumerable<GradedFeatures> houseFeatures = dbContext.GradedFeatures.Where(a => a.HomeId == id);
                dbContext.GradedFeatures.RemoveRange(houseFeatures);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Home> GetAllHomes(int areaId)
        {
            return dbContext.Homes.Where(a => a.AreaId == areaId);
        }

        public Home GetHome(int Id)
        {
            return dbContext.Homes.Find(Id); ;
        }

        public void UpdateHome(Home homeChanges)
        {
            var home = dbContext.Homes.Attach(homeChanges);
            home.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
        }
        public double CalculateGrade(IList<HomeFeatures> gradedFeatures)
        {//this calculates the average grade
            double sum = 0;
            int validGrades = 0;
            for (int i = 0; i < gradedFeatures.Count; i++)
            {
                if (gradedFeatures[i].Grade != null)
                {
                    sum += (double)gradedFeatures[i].Grade;
                    validGrades++;
                }
            }
            if (sum == 0)
            {
                return 0;
            }
            return Math.Round((sum / validGrades), 2);
        }

        public Home AddGradeToHome(int homeId, double grade)
        {
            Home home = dbContext.Homes.Find(homeId);
            home.GeneralGrade = Math.Round(grade,2);
            dbContext.SaveChanges();
            return home;
        }

        public void FillJointTable(CreateHome gradedFeatures)
        {
            var homes = dbContext.GradedFeatures.Where(a => a.HomeId == gradedFeatures.HomeId);
            if (!homes.Any())//if the home hasn't already been graded
            {
                for (int i = 0; i < gradedFeatures.HomeFeatures.Count; i++)
                {
                    GradedFeatures gradedFeature = new GradedFeatures();
                    gradedFeature.HomeId = gradedFeatures.HomeId;
                    gradedFeature.HomeFeaturesId = gradedFeatures.HomeFeatures[i].Id;
                    if (gradedFeatures.HomeFeatures[i].Grade <= 0 || gradedFeatures.HomeFeatures[i].Grade == null)
                    {
                        gradedFeature.Grade = 0;
                    }
                    else if (gradedFeatures.HomeFeatures[i].Grade > 10)
                    {
                        gradedFeature.Grade = 0;
                    }
                    else
                    {
                        gradedFeature.Grade = (double)gradedFeatures.HomeFeatures[i].Grade;
                    }
                    dbContext.GradedFeatures.Add(gradedFeature);
                    dbContext.SaveChanges();
                }
            }
            else
            {
                foreach (var feature in gradedFeatures.HomeFeatures)
                {
                    foreach (var h in homes)
                    {
                        if (h.HomeFeaturesId == feature.Id)
                        {
                            if (feature.Grade <= 0 || feature.Grade == null)
                            {
                                h.Grade = 0;
                            }
                            else if (feature.Grade > 10)
                            {
                                h.Grade = 10;
                            }
                            else
                            {
                                h.Grade = (double)feature.Grade;
                            }
                        }
                    }
                }
                dbContext.SaveChanges();
            }
        }
        public IEnumerable<Home> GetUserHomes(string userId)
        {
            //used toList to force the querry
            IEnumerable<int> areas = dbContext.Areas.Where(a => a.UserId == userId).Select(a => a.Id).ToList<int>();
            IEnumerable<Home> homes = dbContext.Homes.ToList<Home>();
            IList<Home> selectedHomes = new List<Home>();
            foreach(Home home in homes)
            {
                foreach (int area in areas) {
                    if (home.AreaId == area)
                    {
                        selectedHomes.Add(home);
                    }
                }
            }
            return selectedHomes;
        }
    }
}
