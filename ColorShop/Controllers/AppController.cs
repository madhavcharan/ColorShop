using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorShop.Data;
using ColorShop.Services;
using ColorShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorShop.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IPaintingRepository _repository;

        public AppController(IMailService mailService, IPaintingRepository repository)
        {
            _mailService = mailService;
            this._repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("charan.madhav@gmail.com", model.Subject, $"From: { model.Name}, {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Message Sent!";
                ModelState.Clear();
            }
            else
            {
                //Handle Errors
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();

            return View(results);
        }
    }
}