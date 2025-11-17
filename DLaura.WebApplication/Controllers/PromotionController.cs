using DLaura.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DLaura.WebApplication.Controllers
{
    public class PromotionController : Controller
    {
        public IActionResult Index()
        {
            var promotions = new List<Promotion>();

            return View(promotions);
        }
    }
}