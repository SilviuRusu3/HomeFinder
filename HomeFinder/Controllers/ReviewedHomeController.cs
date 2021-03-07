using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.HelpClass;
using HomeFinder.Models;
using HomeFinder.Models.Repository;
using HomeFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinder.Controllers
{
    //using ReviewedHome to avoid confusion with Home.
    public class ReviewedHomeController : Controller
    {
        private readonly IReviewedHomeRepository _homeRepo;
        private readonly IAreasRepository _areaRepo;
        private readonly IFeaturesRepository _featuresRepo;

        public ReviewedHomeController(IReviewedHomeRepository homeRepo, IAreasRepository areaRepository, IFeaturesRepository featuresRepo)
        {
            _homeRepo = homeRepo;
            _areaRepo = areaRepository;
            _featuresRepo = featuresRepo;
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id != null)
            {
                Home model = _homeRepo.GetHome((int)Id);
                Area area = _areaRepo.GetArea(model.AreaId);
                if (area.UserId == this.User.GetUserId())
                {
                    return View(model);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Home home)
        {
            _homeRepo.DeleteHome(home.Id);
            AreaHome areaHome = new AreaHome();
            areaHome.homes = _homeRepo.GetAllHomes(home.AreaId);
            areaHome.area = _areaRepo.GetArea(home.AreaId);
            HomeType type = new HomeType();
            areaHome.homeType = type;
            return View("Index", areaHome);
        }

        [HttpGet]
        public IActionResult Update(int? Id)
        {
            if (Id != null)
            {
                Home model = _homeRepo.GetHome((int)Id);
                Area area = _areaRepo.GetArea(model.AreaId);
                if (area.UserId == this.User.GetUserId())
                {
                    return View(model);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Home updatedHome)
        {
            _homeRepo.UpdateHome(updatedHome);
            return View("Details", updatedHome);
        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                Home model = _homeRepo.GetHome((int)Id);
                Area area = _areaRepo.GetArea(model.AreaId);
                if (area.UserId == this.User.GetUserId())
                {
                    return View(model);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Index(int? Id, HomeType? homeType)
        {
            AreaHome areaHome = new AreaHome();
            HomeType type = new HomeType();
            var allHomes = _homeRepo.GetAllHomes((int)Id);
            areaHome.area = _areaRepo.GetArea((int)Id);
            areaHome.homeType = type;
            if (homeType == null)
            {
                areaHome.homes = allHomes;
            } else
            {
                areaHome.homes = allHomes.Where(h => h.HomeType == homeType || h.HomeType == null);
            }
            return View(areaHome);
        }
        [HttpGet]
        public IActionResult Create(int? Id)
        {
            if (Id != null)
            {
                Area area = _areaRepo.GetArea((int)Id);
                if (area.UserId == this.User.GetUserId() && area != null)
                {
                    Home home = new Home();
                    home.AreaId = (int)Id;
                    return View(home);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Home home)
        {
            if (ModelState.IsValid)
            {
                Home newHome = _homeRepo.AddHome(home);
                CreateHome gradedFeatures = new CreateHome();
                gradedFeatures.AreaId = home.AreaId;
                gradedFeatures.HomeId = newHome.Id;
                gradedFeatures.HomeFeatures = _featuresRepo.GetAllFeatures(this.User.GetUserId()).Where(f => f.HomeType == home.HomeType || f.HomeType == null).ToList();
                return View("GradeHome", gradedFeatures);
            }
            return RedirectToAction("Create", home.AreaId);
        }

        [HttpGet]
        public IActionResult GradeHome(int? Id)
        {
            if (Id != null)
            {
                Home home = _homeRepo.GetHome((int)Id);
                Area area = _areaRepo.GetArea(home.AreaId);
                if (area.UserId == this.User.GetUserId())
                {
                    CreateHome gradedFeatures = new CreateHome();
                    gradedFeatures.AreaId = home.AreaId;
                    gradedFeatures.HomeId = home.Id;
                    gradedFeatures.HomeFeatures = _featuresRepo.GetAllFeatures(this.User.GetUserId()).Where(f => f.HomeType == home.HomeType || f.HomeType == null).ToList();
                    return View(gradedFeatures);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GradeHome(CreateHome gradedHome)
        {
            Home home = _homeRepo.GetHome(gradedHome.HomeId);
            if (ModelState.IsValid)
            {
                double grade = _homeRepo.CalculateGrade(gradedHome.HomeFeatures);
                double areaGrade = (double)_areaRepo.GetArea(gradedHome.AreaId).GeneralGrade;
                if (areaGrade > 0)
                {
                    grade = (grade + areaGrade) / 2;
                }
                Home displayHome = _homeRepo.AddGradeToHome(home.Id, grade);
                _homeRepo.FillJointTable(gradedHome);
                return View("Details", displayHome);
            }
            CreateHome gradedFeatures = new CreateHome();
            gradedFeatures.AreaId = gradedHome.AreaId;
            gradedFeatures.HomeFeatures = _featuresRepo.GetAllFeatures(this.User.GetUserId()).Where(f => f.HomeType == home.HomeType || f.HomeType == null).ToList();
            return View(gradedFeatures);
        }

    }
}
