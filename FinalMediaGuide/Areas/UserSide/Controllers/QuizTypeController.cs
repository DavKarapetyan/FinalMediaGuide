﻿using Microsoft.AspNetCore.Mvc;
using FinalMediaGuide.BLL.Services.Interfaces;


namespace FinalMediaGuide.Areas.UserSide.Controllers
{
    [Area("UserSide")]
    public class QuizTypeController : Controller
    {
        private readonly IQuizTypeService _quiztypeservice;
        public QuizTypeController(IQuizTypeService quiztypeservice)
        {
            _quiztypeservice = quiztypeservice;
        }
    
        public IActionResult Index()
        {
            var data=_quiztypeservice.GetQuizTypes();
            return View(data);
        }
    }
}
