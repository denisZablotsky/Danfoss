﻿using Dunfoss.Data;
using Dunfoss.Models;
using Dunfoss.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dunfoss.Controllers
{
    public class HomeController : Controller
    {
        IFileRepository fileRep = new EfFileRepository();
        ICurrentFile current = new EfCurrentFile();
        private IUserRepository userRepository;
        private ISecurityService _security;

        // GET: Home
        public HomeController()
        {
            //currentFile = new EfCurrentFile();
            //currentFile.InitializeCurrentFile(new CurrentFile());
            //User user = new Models.User();
            //user.Name = "first";
            //user.Password = "12345";
            //EfUserRepository efUserRep = new EfUserRepository();
            //efUserRep.CreateUser(user);
        }
        [HttpGet]
        public ActionResult Change(int id)
        {
            Models.File file =  fileRep.GetFileById(id);
            if(file.Type == 1)
            {
                current.UpdateFile1(file.Path);
            }
            else if (file.Type == 2)
            {
                current.UpdateFile2(file.Path);
            }
            else
            {
                current.UpdateFile3(file.Path);
            }

            return View("GetNav");
        }

        public ActionResult GetFileList()
        {
            IQueryable<Dunfoss.Models.File> list = fileRep.Files;
            return View(list);
        }

        public HomeController(IUserRepository userRep)
        {
            userRepository = userRep;
            _security = new SecurityService();
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public PartialViewResult AuthenticateSection()
        {
            if (_security.IsAuthenticate())
            {
                return PartialView("_authenticatedSection", _security.GetCurrentUser());
            }
            else
            {
                return PartialView("_loginSection", new User());
            }
        }
        
        public ActionResult Login()
        {
            //if (!_security.Authenticate(user.Name, user.Password))
            //    return RedirectToAction("LogIn", "Home");
            //user = userRepository.GetUserByName(user.Name);
            // _security.Login(user);
            // return RedirectToAction("Index", "Home");
            return View("LogIn");
        }
        [HttpPost]
        public ActionResult Logout(User user)
        {
            _security.Logout();
            return RedirectToAction("Index", "Home");
        }
        public ViewResult PhoneNumbersOfStaff()
        {
            return View("PhoneNumbersOfStaff");
        }
        public ViewResult StatisticsOfGroup()
        {
            return View("StatisticsOfGroup");
        }
        public ViewResult LoadOfGroup()
        {
            return View("LoadOfGroup");
        }
        public ViewResult StatusCRM()
        {
            return View("StatusCRM");
        }
        public ViewResult Contacts()
        {
            return View("Contacts");
        }
    }
}