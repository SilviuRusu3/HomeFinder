using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.HelpClass;
using HomeFinder.Models;
using HomeFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinder.Controllers
{
    public class AttributesController : Controller
    {
        private readonly IAttributesRepository _attributesRepo;

        public AttributesController(IAttributesRepository attributesRepo)
        {
            _attributesRepo = attributesRepo;
        }

        public IActionResult General()
        {
            return View();
        }
        public IActionResult Index()
        {
            try
            {
                string userId = this.User.GetUserId();
                var model = _attributesRepo.GetAllAttributes(userId);
                return View(model);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                AttributeDetails model = new AttributeDetails
                {
                    Attributes = _attributesRepo.GetAttribute((int)Id),
                    PageTitle = "Attribute"
                };
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
        public IActionResult Create(LocationAttributes modelAttribute)
        //IActionResult is suitable for ViewResult and RedirectToAction because they implement this interface
        {
            string userId = this.User.GetUserId();
            modelAttribute.UserId = userId;
            if (ModelState.IsValid)//used for validation
            {
                LocationAttributes attribute = _attributesRepo.AddAtribute(modelAttribute);
                return RedirectToAction("Details", new { id = attribute.Id });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                LocationAttributes attribute = _attributesRepo.GetAttribute((int)id);
                if (attribute != null)
                {
                    return View(attribute);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(LocationAttributes attributeChange)
        {
            if (ModelState.IsValid)
            {
                _attributesRepo.UpdateAttribute(attributeChange);
                return RedirectToAction("Details", new { id = attributeChange.Id });
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                LocationAttributes attribute = _attributesRepo.GetAttribute((int)id);
                if (attribute != null)
                {
                    return View(attribute);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(LocationAttributes locationAttributes)
        {
            string userId = this.User.GetUserId();
            _attributesRepo.DeleteAttribute(locationAttributes.Id);
            IEnumerable<LocationAttributes> attributes = _attributesRepo.GetAllAttributes(userId);
            if (attributes != null)
            {
                return View("Index", attributes);
            }
            return NotFound();
        }
    }
}
