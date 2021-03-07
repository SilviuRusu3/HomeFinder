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
    public class AreaController : Controller
    {
        private readonly IAreasRepository _areasRepo;
        private readonly IAttributesRepository _attributesRepo;

        public AreaController(IAreasRepository areasRepo, IAttributesRepository attributesRepo)
        {
            _areasRepo = areasRepo;
            _attributesRepo = attributesRepo;
        }
        public IActionResult Index()
        {
            string userId = this.User.GetUserId();
            var model = _areasRepo.GetAllAreas(userId);
            return View(model);

        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                Area area = _areasRepo.GetArea((int)id);
                if (area != null)
                {
                    return View(area);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Area areaChange)
        {
            if (ModelState.IsValid)
            {
                _areasRepo.UpdateArea(areaChange);
                return RedirectToAction("Details", new { id = areaChange.Id });
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Area area = _areasRepo.GetArea((int)id);
                if (area != null)
                {
                    return View(area);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Area area)
        {
            string userId = this.User.GetUserId();
            _areasRepo.DeleteArea(area.Id);
            IEnumerable<Area> areas = _areasRepo.GetAllAreas(userId);
            if (areas != null)
            {
                return View("Index", areas);
            }
            return NotFound();
        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                Area model = _areasRepo.GetArea((int)Id);
                return View(model);
            }
            return NotFound();
        }
        public ViewResult General()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Area modelArea)
        {
            string userId = this.User.GetUserId();
            modelArea.UserId = userId;
            if (ModelState.IsValid)
            {
                Area area = _areasRepo.AddArea(modelArea);
                return RedirectToAction("CalculateGrade", area);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CalculateGrade(Area area)
        {
            var currentUser = this.User.GetUserId();
            LocationGrades locationGrades = new LocationGrades();
            locationGrades.AreaId = area.Id;
            if (area.GeneralGrade == 0)
            {
                locationGrades.LocationAttributes = _attributesRepo.GetAllAttributes(area.UserId).ToList();
            }
            else
            {
                locationGrades.LocationAttributes = _attributesRepo.GetAllAttributes(currentUser).ToList();
            }
            if (locationGrades != null)
                return View(locationGrades);
            else
                return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculateGrade(LocationGrades locationGrades)
        {
            if (ModelState.IsValid)
            {
                double grade = _areasRepo.CalculateGrade(locationGrades.LocationAttributes);
                var model = _areasRepo.addGradeToArea(locationGrades.AreaId, grade);
                _areasRepo.FillJointTable(locationGrades);
                return View("Details", model);
            }
            LocationGrades locGrades = new LocationGrades();
            Area area = _areasRepo.GetArea(locationGrades.AreaId);
            locGrades.AreaId = area.Id;
            locGrades.LocationAttributes = _attributesRepo.GetAllAttributes(area.UserId).ToList();
            return View(locGrades);
        }
    }
}
