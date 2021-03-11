using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeFinder.Models;
using Microsoft.AspNetCore.Authorization;
using HomeFinder.Models.Repository;
using HomeFinder.HelpClass;
using Microsoft.AspNetCore.Identity;

namespace HomeFinder.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewedHomeRepository _reviewedHomeRepo;
        private readonly IAreasRepository _areasRepository;
        private readonly SignInManager<User> signInManager;

        public HomeController(ILogger<HomeController> logger, IReviewedHomeRepository reviewedHomeRepo, IAreasRepository areasRepository, SignInManager<User> signInManager)
        {
            _logger = logger;
            _reviewedHomeRepo = reviewedHomeRepo;
            _areasRepository = areasRepository;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                IEnumerable<Home> homes = _reviewedHomeRepo.GetUserHomes(this.User.GetUserId());
                List<Home> sortedHomes = homes.ToList();
                sortedHomes.Sort(new HomeComparer());
                return View(sortedHomes);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
