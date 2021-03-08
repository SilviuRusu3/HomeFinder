using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.HelpClass;
using HomeFinder.Models;
using HomeFinder.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinder.Controllers
{
    public class FeaturesController : Controller
    {
        public readonly IFeaturesRepository _featuresRepo;

        public FeaturesController(IFeaturesRepository featuresRepo)
        {
            _featuresRepo = featuresRepo;
        }

        public IActionResult General()
        {
            return View();
        }
        public IActionResult Index()
        {
            string userId = this.User.GetUserId();
            var model = _featuresRepo.GetAllFeatures(userId);
            return View(model);
        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                HomeFeatures model = _featuresRepo.GetFeature((int)Id);
                return View(model);
            }
            return NotFound();

        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeFeatures modelFeature)
        {
            string userId = this.User.GetUserId();
            modelFeature.UserId = userId;
            if (ModelState.IsValid)
            {
                HomeFeatures feature = _featuresRepo.AddFeature(modelFeature);
                return RedirectToAction("Details", new { id = feature.Id });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                HomeFeatures feature = _featuresRepo.GetFeature((int)id);
                if (feature != null)
                {
                    return View(feature);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(HomeFeatures featureChange)
        {
            if (ModelState.IsValid)
            {
                _featuresRepo.UpdateFeature(featureChange);
                return RedirectToAction("Details", new { id = featureChange.Id });
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                HomeFeatures feature = _featuresRepo.GetFeature((int)id);
                if (feature != null)
                {
                    return View(feature);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(HomeFeatures feature)
        {
            string userId = this.User.GetUserId();
            _featuresRepo.DeleteFeature(feature.Id);
            IEnumerable<HomeFeatures> features = _featuresRepo.GetAllFeatures(userId);
            if (features != null)
            {
                return View("Index", features);
            }
            return NotFound();
        }
    }
}
